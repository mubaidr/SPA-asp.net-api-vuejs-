<template>
  <div class="card-cont">
    <md-card class="md-card-custom" md-hover>
      <md-card-header>
        <md-card-header-text>
          <div class="md-title" v-on:click="viewDetails()">
            {{Task.Title}}</div>
          <div class="md-subhead" v-on:click="viewDetails()">
            <md-tooltip md-direction="top">Task is assigned by this user</md-tooltip>
            {{Task.AssignedBy.Email}}
          </div>
        </md-card-header-text>
      </md-card-header>
      <md-card-content style="padding-bottom: 0">
        <span v-on:click="viewDetails()">{{Task.Description || "No Description Provided."}}</span>
      </md-card-content>
      <md-card-content>
        <div class="card-date" title="Due Date" :class="typeClass()">
          <span class="text-muted">{{formatDate(Task.DateDue)}}</span>
        </div>
        <md-tooltip md-direction="top">Progress and Status</md-tooltip>
        <md-progress :md-theme="typeClass()" :md-progress="Task.Progress"></md-progress>
      </md-card-content>
      <md-card-content>
        <md-tooltip md-direction="top">Task is assigned to followiung users</md-tooltip>
        <div class="md-caption" v-show="Task.AssignedTo.length">
          <span class="chip-custom" v-for="user in Task.AssignedTo" :key="user.Email">{{user.Email}}</span>
        </div>
        <div class="md-caption" v-show="!Task.AssignedTo.length">
          <span class="chip-custom">Self</span>
        </div>
      </md-card-content>
      <md-card-actions class="custom-footer">
        <div v-show="isSelfCreated">
          <md-button class="md-icon-button" @click.native="restoreTask">
            <md-tooltip md-direction="top">Restore</md-tooltip>
            <md-icon>unarchive</md-icon>
          </md-button>
        </div>
      </md-card-actions>
    </md-card>
    <md-dialog-alert md-content="Task has been restored." md-ok-text="Ok" ref="dialog-success" @close="dialogSuccessClose">
    </md-dialog-alert>
  </div>
</template>
<script>
import {
  restore
} from 'services/tasks'
import moment from 'moment'

export default {
  name: 'task-card',
  props: ['Task'],
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
      } else {
        return 'theme-primary'
      }
    },
    formatDate (date) {
      return moment(date).format('hh:mmA DD-MM-YY')
    },
    refConfirm () {
      return `ref-confirm-${this.Task.MainTaskID}`
    },
    dialogSuccessClose () {
      const TaskId = this.Task.MainTaskID
      this.$emit('remove-task-item', {
        id: TaskId
      })
    },
    viewDetails () { },
    confirmRestore () {
      this.$refs[this.refConfirm()].open()
    },
    restoreTask (type) {
      const TaskId = this.Task.MainTaskID

      restore({
        id: TaskId
      }).then(res => {
        this.$refs['dialog-success'].open()
      }).catch(err => {
        console.dir(err)
      })
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
    min-width: 280px;
    width: 98%;
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
