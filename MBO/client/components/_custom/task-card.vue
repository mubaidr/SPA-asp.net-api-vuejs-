<template>
  <md-layout md-flex="20" md-flex-xsmall="100" md-flex-small="50" md-flex-medium="33" class="card">
    <md-card md-with-hover class="md-card-custom">
      <md-card-header>
        <md-card-header-text>
          <div class="md-title">
            {{Task.Title}}</div>
          <div class="md-subhead">
            {{Task.AssignedBy.UserName}}
          </div>
        </md-card-header-text>
        <md-menu md-size="3" md-direction="bottom left">
          <md-button class="md-icon-button" md-menu-trigger>
            <md-icon>more_vert</md-icon>
          </md-button>
          <md-menu-content>
            <md-menu-item>
              <span>View Details</span>
              <md-icon>phone</md-icon>
            </md-menu-item>
            <md-menu-item>
              <span>Add Comment</span>
              <md-icon>message</md-icon>
            </md-menu-item>
          </md-menu-content>
        </md-menu>
      </md-card-header>
      <md-card-content style="padding-bottom: 0">
        <span>{{Task.Description || "No Description Provided."}}</span>
        <div class="md-caption" v-show="Task.AssignedTo.length">
          Assigned To:
          <ul>
            <li v-for="user in Task.AssignedTo">{{user}}</li>
          </ul>
        </div>
      </md-card-content>
      <md-card-content>
        <div class="card-date" :class="type_class" title="Due Date">
          <md-icon :class="type_animate">{{type_icon}}</md-icon>
          <span class="text-muted">{{formatedDueDate}}</span>
        </div>
        <md-progress :md-theme="type_class" :md-progress="Task.Progress"></md-progress>
      </md-card-content>
    </md-card>
  </md-layout>
</template>
<script>
  import moment from 'moment';

  export default {
    name: 'task-card',
    props: ['Task'],
    computed: {
      type_class: function () {
        const _self = this;

        const now = moment();
        const dueDate = moment(_self.Task.DateDue);
        const diff = now.diff(dueDate, 'days');

        if (_self.Task.Progress == 100) {
          return 'theme-success';
        }
        if (diff < 0) {
          return 'theme-danger';
        } else if (diff < 2) {
          return 'theme-warn';
        } else if (diff < 5) {
          return 'theme-normal';
        } else {
          return 'theme-primary';
        }
      },
      type_animate: function () {
        const _self = this;

        const now = moment();
        const dueDate = moment(_self.Task.DateDue);
        const diff = now.diff(dueDate, 'days');

        if (diff < 0) {
          return 'animate-danger';
        } else if (diff < 2) {
          return 'animate-warn';
        } else {
          return '';
        }
      },
      type_icon: function () {
        switch (this.type_class) {
          case 'theme-success':
            return 'done';
            break;
          case 'theme-danger':
            return 'warning';
            break;
          case 'theme-warn':
            return 'av_timer';
            break;
          case 'theme-normal':
            return 'timelapse';
            break;
          case 'theme-primary':
          default:
            return 'timer';
            break;
        }
      },
      formatedDueDate: function () {
        return moment(this.Task.DateDue).format('HH:mm A [-] DD-MM-YYYY');
      }
    },
    methods: {

    },
    mounted: function () {
      //this.Task.AssignedTo = ['Some', 'user', 'more', 'user'];      
    }
  }

</script>
<style scoped>
  .card {
    margin-bottom: 10px;
  }
  
  .card-date {
    margin-bottom: 10px;
  }
  
  .md-card-custom {
    width: 99%;
    overflow-x: hidden;
  }
  
  .md-caption {
    margin-top: 10px;
  }
  
  .md-caption ul {
    list-style: square;
    padding-left: 15px;
    margin: 0;
    margin-top: 5px;
  }
  
  .text-muted {
    opacity: 0.6;
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
  
  .animate-warn {
    animation-delay: 2s;
    -webkit-animation-name: jump;
    animation-name: jump;
    -webkit-animation-duration: 8s;
    animation-duration: 8s;
    -webkit-animation-fill-mode: both;
    animation-fill-mode: both;
    animation-iteration-count: infinite;
    transform-origin: 50%;
  }
  
  .animate-danger {
    animation-delay: 1s;
    -webkit-animation-name: shake;
    animation-name: shake;
    -webkit-animation-duration: 5s;
    animation-duration: 5s;
    -webkit-animation-fill-mode: both;
    animation-fill-mode: both;
    animation-iteration-count: infinite;
    transform-origin: 50% 0;
  }

</style>
