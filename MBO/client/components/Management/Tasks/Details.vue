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
    <md-layout :md-gutter="24">
      <md-layout md-flex="50" md-flex-small="100">
        <h3>Comments</h3>
        <ul class="full-width comment-list">
          <li class="no-border">
            <md-input-container>
              <label>Add Comment</label>
              <md-textarea maxlength="100"></md-textarea>
            </md-input-container>
          </li>
          <li v-for="comment in log.content">
            <!--TODO Align to right is self reply-->
            <p>{{comment.Description}}</p>
            <span>{{comment.ApplicationUser.UserName}}</span>
            <span>{{formatDate(comment.LogTime)}}</span>
          </li>
        </ul>
      </md-layout>
      <md-layout md-flex="50" md-flex-small="100">
        <h3>Progress updates</h3>
      </md-layout>
    </md-layout>
    <pre>{{log.content}}</pre>
    <pre>{{progressHistory.content}}</pre>
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
    watch: {
      'log.content': function () {
        this.$set(this.log, 'loading', false);
      },
      'progressHistory.content': function () {
        this.$set(this.progressHistory, 'loading', false);
      }
    },
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
        const _self = this;
        _self.$set(_self.log, 'loading', false);

        getLog().then(function (res) {
          _self.$set(_self.log, 'content', res.data);
        }).catch(function (err) {
          console.dir(err);
        });
      },
      loadProgressHistory: function () {
        const _self = this;
        _self.$set(_self.progressHistory, 'loading', false);

        getProgressHistory().then(function (res) {
          _self.$set(_self.progressHistory, 'content', res.data);
        }).catch(function (err) {
          console.dir(err);
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
  
  .comment-list {
    margin: 0;
    padding: 0;
    list-style: none;
  }
  
  .comment-list li {
    border-bottom: 1px solid #aaa;
    border-color: rgba(0, 0, 0, .12);
    margin: 0;
    padding: 20px 0;
  }
  
  .comment-list li.no-border {
    padding-bottom: 0;
  }
  
  .comment-list p {
    margin-top: 0;
    padding-top: 0;
    text-align: left;
  }
  
  .comment-list span {
    color: #aaa;
    color: rgba(0, 0, 0, 0.5);
    display: block;
    clear: both;
  }
  
  .comment-list span {
    font-size: 0.75em;
  }

</style>
