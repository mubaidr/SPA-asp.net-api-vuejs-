<template>
  <div>
    <span class="md-headline">{{Task.Title}}</span><br/>
    <span class="md-subheading">{{Task.AssignedBy.UserName}}</span>
    <br/><br/>
    <md-layout :md-gutter="24">
      <md-layout md-flex="20" md-flex-small="50" md-flex-xsmall="100">
        <span class="md-subheading"><span class="md-caption">Description:<br/></span> {{Task.Description}}</span>
      </md-layout>
      <md-layout md-flex="20" md-flex-small="50" md-flex-xsmall="100">
        <span class="md-subheading"><span class="md-caption">Due Date:<br/></span> {{formatDate(Task.DateDue)}}
        </span>
      </md-layout>
      <md-layout md-flex="20" md-flex-small="50" md-flex-xsmall="100">
        <span class="md-subheading"><span class="md-caption">Assigned On:<br/></span> {{formatDate(Task.DateAssigned)}}</span>
      </md-layout>
      <md-layout md-flex="15" md-flex-small="50" md-flex-xsmall="100">
        <div class="md-subheading"><span class="md-caption">Assigned To:<br/></span>
          <div class="md-caption" v-show="Task.AssignedTo.length">
            <span class="chip-custom" v-for="user in Task.AssignedTo">{{user.Email}}</span>
          </div>
          <div class="md-caption" v-show="!Task.AssignedTo.length">
            <span class="chip-custom">Self</span>
          </div>
        </div>
      </md-layout>
      <md-layout md-flex="5" md-flex-small="50" md-flex-xsmall="100">
        <span class="md-subheading"><span class="md-caption">Status:<br/></span>
        <div :class="type_class()">
          <md-icon :class="type_animate()">{{type_icon()}}</md-icon>
        </div>
      </md-layout>
      <md-layout md-flex="20" md-flex-small="50" md-flex-xsmall="100">
        <span class="md-subheading"><span class="md-caption">Progress:</span> {{Task.Progress}}%</span>
        <md-progress :md-theme="type_class()" :md-progress="Task.Progress"></md-progress>
      </md-layout>
    </md-layout>
    <hr/>
    <md-layout md-flex="60" :md-gutter="24">
      <md-layout md-flex-small="100">
        <span class="md-subheading">Comments</span>
        <md-table v-once>
          <md-table-header>
            <md-table-row>
              <md-table-head>Dessert (100g serving)</md-table-head>
              <md-table-head md-numeric>Calories (g)</md-table-head>
              <md-table-head md-numeric>Fat (g)</md-table-head>
              <md-table-head md-numeric>Carbs (g)</md-table-head>
              <md-table-head md-numeric>Protein (g)</md-table-head>
            </md-table-row>
          </md-table-header>
          <md-table-body>
            <md-table-row v-for="(row, index) in 5" :key="index">
              <md-table-cell>Dessert Name</md-table-cell>
              <md-table-cell v-for="(col, index) in 4" :key="index" md-numeric>10</md-table-cell>
            </md-table-row>
          </md-table-body>
        </md-table>
      </md-layout>
      <md-layout md-flex="40" md-flex-small="100">
        <span class="md-subheading">Progress updates</span>
        <md-table v-once>
          <md-table-header>
            <md-table-row>
              <md-table-head>Dessert (100g serving)</md-table-head>
              <md-table-head md-numeric>Calories (g)</md-table-head>
              <md-table-head md-numeric>Fat (g)</md-table-head>
              <md-table-head md-numeric>Carbs (g)</md-table-head>
              <md-table-head md-numeric>Protein (g)</md-table-head>
            </md-table-row>
          </md-table-header>
          <md-table-body>
            <md-table-row v-for="(row, index) in 5" :key="index">
              <md-table-cell>Dessert Name</md-table-cell>
              <md-table-cell v-for="(col, index) in 4" :key="index" md-numeric>10</md-table-cell>
            </md-table-row>
          </md-table-body>
        </md-table>
      </md-layout>
    </md-layout>
  </div>
</template>
<script>
  import {
    getDetails
  } from 'services/tasks';
  import {
    getLog
  } from 'services/taskLog';
  import {
    getProgressHistory
  } from 'services/progressHistory';

  import moment from 'moment';

  export default {
    data: function () {
      return {
        Task: {
          "AssignedBy": {
            "Claims": [],
            "Logins": [],
            "Roles": [],
            "FirstName": null,
            "LastName": null,
            "FullName": null,
            "Email": "tester@test.com",
            "EmailConfirmed": false,
            "PasswordHash": "AAfNlYFPlbFENjLpOXQXzgm513SNYAoLZWlId5Md7j3lIFLOQaKZKNl0wk7O1lVkpA==",
            "SecurityStamp": "1292a83d-745b-4570-954b-015527ff1a42",
            "PhoneNumber": null,
            "PhoneNumberConfirmed": false,
            "TwoFactorEnabled": false,
            "LockoutEndDateUtc": null,
            "LockoutEnabled": false,
            "AccessFailedCount": 0,
            "Id": "c9c03a40-d155-45cf-abd5-48f8cc4cf8e8",
            "UserName": "tester@test.com"
          },
          "Category": {
            "CategoryID": 3,
            "Title": "Placement",
            "Description": "Placement"
          },
          "Status": {
            "StatusID": 1,
            "Title": "Assigned",
            "Description": "Task has been created",
            "Level": 0
          },
          "MainTaskID": 3,
          "Title": "OOOOOOO",
          "Description": "ssss",
          "IsDeleted": false,
          "Progress": 33,
          "DateAssigned": "2017-01-11T12:34:08.537",
          "DateDue": "2017-12-01T12:33:00",
          "StatusID": 1,
          "CategoryID": 3,
          "AssignedByID": "c9c03a40-d155-45cf-abd5-48f8cc4cf8e8",
          "AssignedTo": [{
            "Claims": [],
            "Logins": [],
            "Roles": [],
            "FirstName": null,
            "LastName": null,
            "FullName": null,
            "Email": "tester@test.com",
            "EmailConfirmed": false,
            "PasswordHash": "AAfNlYFPlbFENjLpOXQXzgm513SNYAoLZWlId5Md7j3lIFLOQaKZKNl0wk7O1lVkpA==",
            "SecurityStamp": "1292a83d-745b-4570-954b-015527ff1a42",
            "PhoneNumber": null,
            "PhoneNumberConfirmed": false,
            "TwoFactorEnabled": false,
            "LockoutEndDateUtc": null,
            "LockoutEnabled": false,
            "AccessFailedCount": 0,
            "Id": "c9c03a40-d155-45cf-abd5-48f8cc4cf8e8",
            "UserName": "tester@test.com"
          }]
        },
        Type: 'view',
        loading: true,
        users: [],
        log: {
          content: [],
          loading: true
        },
        progressHistory: {
          content: [],
          loading: true
        }
      }
    },
    watch: {},
    methods: {
      formatDate: function (date) {
        return moment(date).format('HH:mmA DD-MM-YY');
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
        switch (this.type_class()) {
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
      loadLog: function () {
        getLog().then(function (res) {
          console.log(res);
        }).catch(function (err) {
          console.log(err);
        });
      },
      loadProgressHistory: function () {
        getProgressHistory().then(function (res) {
          console.log(res);
        }).catch(function (err) {
          console.log(err);
        });
      }
    },
    mounted: function () {
      const _self = this;
      const task = _self.$route.params.Task;
      const type = _self.$route.params.Type;
      /*
      if (task) {
        _self.$set(_self, 'Type', type);
        _self.$set(_self, 'Task', task);
      } else {
        _self.$router.push({
          path: '/tasks'
        });
      }
    */
      //TODO fetch comments, progress history

      _self.loadLog();
      _self.loadProgressHistory();
    }
  }

</script>
<style scoped>
  .chip-custom {
    background-color: rgba(0, 0, 0, 0.1);
    padding: 5px;
    border-radius: 2px;
    margin-right: 2px;
  }
  
  hr {
    margin: 25px 0;
    border-color: #eee;
    border-color: rgba(0, 0, 0, 0.05);
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
