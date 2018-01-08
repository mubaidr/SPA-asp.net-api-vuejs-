<template>
  <div class="card-cont">
    <md-card class="md-card-custom" md-with-hover>
      <md-card-header>
        <md-card-header-text>
          <div class="md-title text-primary">
            {{task.Title}}
          </div>
          <div class="md-subhead">
            {{task.AssignedBy.Email}}
          </div>
        </md-card-header-text>
        <md-menu md-size="3" md-direction="bottom left" v-show="isSelfCreated">
          <md-button class="md-icon-button" md-menu-trigger>
            <md-icon>more_vert</md-icon>
          </md-button>
          <md-menu-content>
            <md-menu-item>
              <span>Update</span>
              <md-icon>mode_edit</md-icon>
            </md-menu-item>
            <md-menu-item @click.native="confirmDelete">
              <span>Delete</span>
              <md-icon>delete</md-icon>
            </md-menu-item>
          </md-menu-content>
        </md-menu>
      </md-card-header>
      <md-card-content>
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
        <md-button class="md-icon-button" @click.native="viewDetails('comment')">
          <md-icon>chat</md-icon>
        </md-button>
      </md-card-actions>
    </md-card>
  </div>
</template>
<script>
  import moment from 'moment'

  export default {
    name: 'task-card',
    props: ['task', 'type'],
    data () {
      return {}
    },
    computed: {
      isSelfCreated () {
        return this.$store.getters.userInfo.Email === this.task.AssignedBy.Email
      }
    },
    methods: {
      formatDate (date) {
        return moment(date).format('hh:mmA DD-MM-YY')
      },
      typeAnimate () {
        const now = moment()
        const dueDate = moment(this.task.DateDue)
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
        }
        return 'theme-primary'
      },
      viewDetails (obj) {
        // eslint-disable-next-line
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
      }
    }
  }
</script>
<style>

</style>
