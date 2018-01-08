<template>
  <div class="list-item-cont">
    <md-list-item class="li-custom">
      <div class="md-list-text-container">
        <span>{{Task.Title}}
          <span class="text-small"> by {{Task.AssignedBy.Email}} at {{formatDate(Task.DateDue)}}</span>
        </span>
        <span></span>
        <p>{{Task.Description || "No Description Provided."}}</p>
        <span class="text-small">{{formatDate(Task.DateDue)}}</span>
        <md-progress :md-theme="typeClass()" :md-progress="Task.Progress"></md-progress>
      </div>
      <div class="card-date" :class="typeClass()" title="Due Date">
        <md-icon :class="typeAnimate()" class="pull-right">{{typeIcon()}}</md-icon>
      </div>
      <md-menu md-size="3" md-direction="bottom left" v-show="isSelfCreated">
        <md-button class="md-icon-button" md-menu-trigger>
          <md-icon>more_vert</md-icon>
        </md-button>
        <md-menu-content>
          <md-menu-item @click.native="confirmDelete">
            <span>Delete</span>
            <md-icon>delete</md-icon>
          </md-menu-item>
          <md-menu-item>
            <span>Update</span>
            <md-icon>mode_edit</md-icon>
          </md-menu-item>
        </md-menu-content>
      </md-menu>
    </md-list-item>
    <!--Delete Confirmation-->
    <md-dialog :md-close-to="DialogCloseTarget" :ref="refConfirm">
      <md-dialog-title>Move to Archive</md-dialog-title>
      <md-dialog-content>Are you sure you want to archive this task?</md-dialog-content>
      <md-dialog-actions>
        <md-button class="md-primary" @click.native="onDeleteClose('cancel')">Wait... that was a mistake!</md-button>
        <md-button class="md-primary" @click.native="onDeleteClose('ok')">Sure</md-button>
      </md-dialog-actions>
    </md-dialog>
  </div>
</template>
<script>
  import { remove } from 'services/tasks'
  import moment from 'moment'

  export default {
    name: 'task-list-item',
    props: ['Task', 'Type'],
    data () {
      return {
        DialogCloseTarget: null
      }
    },
    computed: {
      isSelfCreated () {
        return (
          this.$store.getters.getUserInfo.Email === this.Task.AssignedBy.Email
        )
      }
    },
    methods: {
      formatDate (date) {
        return moment(date).format('hh:mmA DD-MM-YY')
      },
      refConfirm () {
        return `ref-confirm-${this.Task.MainTaskID}`
      },
      typeAnimate () {
        const now = moment()
        const dueDate = moment(this.Task.DateDue)
        const diff = now.diff(dueDate, 'days')

        if (diff < 0) {
          return 'animate-danger'
        } else if (diff < 2) {
          return 'animate-warn'
        }
        return ''
      },
      typeIcon () {
        switch (this.typeClass()) {
          case 'theme-success':
            return 'done'
          case 'theme-danger':
            return 'warning'
          case 'theme-warn':
            return 'av_timer'
          case 'theme-normal':
            return 'timelapse'
          case 'theme-primary':
          default:
            return 'timer'
        }
      },
      typeClass () {
        const now = moment()
        const dueDate = moment(this.Task.DateDue)
        const diff = now.diff(dueDate, 'days')

        if (this.Task.Progress === 100) {
          return 'theme-success'
        }
        if (diff < 0) {
          return 'theme-danger'
        } else if (diff < 2) {
          return 'theme-warn'
        } else if (diff < 5) {
          return 'theme-normal'
        }
        return 'theme-primary'
      },
      viewDetails (obj) {
        // const TaskId = this.Task.MainTaskID

        // eslint-disable-next-line
        if (!obj) obj = 'view'

        const url = {
          // named route required for sending params
          name: 'task-details',
          params: {
            Task: this.Task,
            Type: obj
          }
        }

        this.$router.push(url)
      },
      confirmDelete () {
        this.$refs[this.refConfirm()].open()
      },
      onDeleteClose (type) {
        const TaskId = this.Task.MainTaskID

        if (type === 'ok') {
          remove({
            id: TaskId
          })
            .then(() => {
              this.DialogCloseTarget = '#btn-view-trash'
              this.animateTrashButton()
              this.$refs[this.refConfirm()].close()

              window.setTimeout(() => {
                this.$emit('remove-task-item', {
                  id: TaskId,
                  type: this.Type
                })
              }, 250)
            })
            .catch(err => {
              console.dir(err)
            })
        } else {
          this.DialogCloseTarget = null
          this.$refs[this.refConfirm()].close()
        }
      },
      animateTrashButton () {
        window.setTimeout(() => {
          document.getElementById('btn-view-trash').className += ' animate-active'
          window.setTimeout(() => {
            document.getElementById(
              'btn-view-trash'
            ).className = document
              .getElementById('btn-view-trash')
              .className.replace(/(?:^|\s)animate-active(?!\S)/g, '')
          }, 250)
        }, 200)
      }
    },
    mounted () {}
  }
</script>
<style>
  .list-item-cont {
    margin: 0 auto;
    display: block;
  }

  .list-item-cont li {
    padding: 10px 0;
    border: 1px solid #efefef !important;
  }

  .text-muted {
    opacity: 0.6;
  }

  .text-small {
    font-size: 60%;
  }

  .theme-danger .md-icon {
    color: #f44336;
  }

  .theme-warn .md-icon {
    color: #ff5722;
  }

  .theme-normal .md-icon {
    color: #ff9800;
  }

  .theme-primary .md-icon {
    color: #cddc39;
  }

  .theme-success .md-icon {
    color: #4caf50;
  }

  .card-cont .custom-footer {
    visibility: hidden;
  }

  .card-cont:hover .custom-footer {
    visibility: visible;
  }
</style>
