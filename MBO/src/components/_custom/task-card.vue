<template>
  <div class="card-cont">
    <md-card class="md-card-custom">
      <md-ink-ripple />
      <md-card-header>
        <md-card-header-text>
          <div class="md-title text-primary" v-on:click="viewDetails()">
            {{Task.Title}}</div>
          <div class="md-subhead" v-on:click="viewDetails()">
            {{Task.AssignedBy.Email}}
          </div>
        </md-card-header-text>
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
      </md-card-header>
      <md-card-content style="padding-bottom: 0">
        <span v-on:click="viewDetails()">{{Task.Description || "No Description Provided."}}</span>
      </md-card-content>
      <md-card-content>
        <div class="card-date" :class="typeClass()" title="Due Date">
          <span class="text-muted">{{formatDate(Task.DateDue)}}</span>
          <md-icon :class="typeAnimate()" class="pull-right">{{typeIcon()}}</md-icon>
        </div>
        <md-progress :md-theme="typeClass()" :md-progress="Task.Progress"></md-progress>
      </md-card-content>
      <md-card-content>
        <div class="md-caption" v-show="Task.AssignedTo.length">
          <span class="chip-custom" v-for="user in Task.AssignedTo">{{user.Email}}</span>
        </div>
        <div class="md-caption" v-show="!Task.AssignedTo.length">
          <span class="chip-custom">Self</span>
        </div>
      </md-card-content>
      <md-card-actions class="custom-footer">
        <div v-show="isSelfCreated">
          <md-menu md-direction="bottom left">
            <md-button class="md-icon-button" md-menu-trigger>
              <md-icon>trending_up</md-icon>
            </md-button>
            <md-menu-content>
              <md-menu-item>My Item 1</md-menu-item>
              <md-menu-item>My Item 2</md-menu-item>
              <md-menu-item disabled>My Item 3</md-menu-item>
              <md-menu-item>My Item 4</md-menu-item>
            </md-menu-content>
          </md-menu>
        </div>
        <md-button class="md-icon-button" @click.native="viewDetails('comment')">
          <md-icon>chat</md-icon>
        </md-button>
      </md-card-actions>
    </md-card>
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
  import {
    remove
  } from 'services/tasks'
  import moment from 'moment'

  export default {
    name: 'task-card',
    props: ['Task', 'Type'],
    data () {
      return {
        DialogCloseTarget: null
      }
    },
    computed: {
      isSelfCreated () {
        return this.$store.getters.getUserInfo.Email === this.Task.AssignedBy.Email
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
        const _self = this

        const now = moment()
        const dueDate = moment(_self.Task.DateDue)
        const diff = now.diff(dueDate, 'days')

        if (diff < 0) {
          return 'animate-danger'
        } else if (diff < 2) {
          return 'animate-warn'
        } else {
          return ''
        }
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
        const _self = this

        const now = moment()
        const dueDate = moment(_self.Task.DateDue)
        const diff = now.diff(dueDate, 'days')

        if (_self.Task.Progress === 100) {
          return 'theme-success'
        }
        if (diff < 0) {
          return 'theme-danger'
        } else if (diff < 2) {
          return 'theme-warn'
        } else if (diff < 5) {
          return 'theme-normal'
        } else {
          return 'theme-primary'
        }
      },
      viewDetails (obj) {
        const _self = this
        // const TaskId = _self.Task.MainTaskID
        if (!obj) obj = 'view'

        const url = {
          // named route required for sending params
          name: 'task-details',
          params: {
            Task: _self.Task,
            Type: obj
          }
        }

        this.$router.push(url)
      },
      confirmDelete () {
        this.$refs[this.refConfirm()].open()
      },
      onDeleteClose (type) {
        const _self = this
        const TaskId = _self.Task.MainTaskID

        if (type === 'ok') {
          remove({
            id: TaskId
          }).then(res => {
            _self.DialogCloseTarget = '#btn-view-trash'
            _self.animateTrashButton()
            _self.$refs[_self.refConfirm()].close()

            window.setTimeout(() => {
              _self.$emit('remove-task-item', {
                id: TaskId,
                type: _self.Type
              })
            }, 250)
          }).catch(err => {
            console.dir(err)
          })
        } else {
          _self.DialogCloseTarget = null
          _self.$refs[_self.refConfirm()].close()
        }
      },
      animateTrashButton () {
        window.setTimeout(() => {
          document.getElementById('btn-view-trash').className += ' animate-active'
          window.setTimeout(() => {
            document.getElementById('btn-view-trash').className = document.getElementById('btn-view-trash').className
              .replace(/(?:^|\s)animate-active(?!\S)/g, '')
          }, 250)
        }, 200)
      }
    },
    mounted () { }
  }

</script>
<style scoped>
  .card-cont {
    margin: 0 10px 10px 0;
  }
  
  .card-date {
    margin-bottom: 10px;
  }
  
  .md-card-custom {
    min-width: 295px;
    width: 99%;
    overflow-x: hidden;
  }
  
  .md-card-custom .md-card-content:last-child {
    padding-top: 0!important;
  }
  
  .md-caption ul {
    list-style: none;
    padding-left: 0;
    margin: 0;
    margin-top: 5px;
  }
  
  .text-muted {
    opacity: 0.6;
  }
  
  .chip-custom {
    background-color: rgba(0, 0, 0, 0.1);
    padding: 5px;
    border-radius: 2px;
    margin-right: 2px;
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

</style>
