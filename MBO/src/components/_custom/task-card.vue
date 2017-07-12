<template>
  <div class="card-cont">
    <md-card class="md-card-custom" md-hover>
      <md-card-header>
        <md-card-header-text>
          <div class="md-title text-primary">
            {{task.Title}}</div>
          <div class="md-subhead">
            {{task.AssignedBy.Email}}
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
        <span>{{task.Description || "No Description Provided."}}</span>
      </md-card-content>
      <md-card-content>
        <div class="card-date" title="Due Date" :class="typeClass()">
          <span class="text-muted">{{formatDate(task.DateDue)}}</span>
          <md-icon class="pull-right" :class="typeAnimate()">{{typeIcon()}}</md-icon>
        </div>
        <md-progress :md-theme="typeClass()" :md-progress="task.Progress"></md-progress>
      </md-card-content>
      <md-card-content>
        <template v-if="task.AssignedTo.length">
          <span class="chip-custom" v-for="user in task.AssignedTo" :key="user.Email">{{user.Email}}</span>
        </template>
        <span class="chip-custom" v-else>Self</span>
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
              <md-menu-item disabled="">My Item 3</md-menu-item>
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
    <!-- <md-dialog :md-close-to="DialogCloseTarget" :ref="refConfirm">
                                      <md-dialog-title>Move to Archive</md-dialog-title>
                                      <md-dialog-content>Are you sure you want to archive this task?</md-dialog-content>
                                      <md-dialog-actions>
                                        <md-button class="md-primary" @click.native="onDeleteClose('cancel')">Wait... that was a mistake!</md-button>
                                        <md-button class="md-primary" @click.native="onDeleteClose('ok')">Sure</md-button>
                                      </md-dialog-actions>
                                    </md-dialog> -->
  </div>
</template>
<script>
import {
  remove
} from 'services/tasks'
import moment from 'moment'

export default {
  name: 'task-card',
  props: ['task', 'type'],
  data () {
    return {
      DialogCloseTarget: null
    }
  },
  computed: {
    isSelfCreated () {
      return this.$store.getters.getUserInfo.Email === this.task.AssignedBy.Email
    }
  },
  methods: {
    formatDate (date) {
      return moment(date).format('hh:mmA DD-MM-YY')
    },
    refConfirm () {
      return `ref-confirm-${this.task.MainTaskID}`
    },
    typeAnimate () {
      const now = moment()
      const dueDate = moment(this.task.DateDue)
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
      const now = moment()
      const dueDate = moment(this.task.DateDue)
      const diff = now.diff(dueDate, 'days')

      if (this.task.Progress === 100) {
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
      if (!obj) obj = 'view'

      const url = {
        // named route required for sending params
        name: 'task-details',
        params: {
          task: this.task,
          type: obj
        }
      }

      this.$router.push(url)
    },
    confirmDelete () {
      this.$refs[this.refConfirm()].open()
    },
    onDeleteClose (type) {
      const TaskId = this.task.MainTaskID

      if (type === 'ok') {
        remove({
          id: TaskId
        }).then(res => {
          this.DialogCloseTarget = '#btn-view-trash'
          this.$refs[this.refConfirm()].close()

          window.setTimeout(() => {
            this.$emit('remove-task-item', {
              id: TaskId,
              type: this.type
            })
          }, 250)
        }).catch(err => {
          console.dir(err)
        })
      } else {
        this.DialogCloseTarget = null
        this.$refs[this.refConfirm()].close()
      }
    }
  }
}

</script>
<style>

</style>
