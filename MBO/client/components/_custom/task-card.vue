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
        <div class="card-date" title="Due Date">
          <md-icon :class="type_class">access_time</md-icon>
          <span>{{formatedDueDate}}</span>
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
    margin-bottom: 20px;
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

</style>
