<template>
  <div>
    <span class="md-headline">{{Task.Title}}</span><br/>
    <span class="md-subheading">{{Task.AssignedBy.UserName}}</span><br/>
    <span class="md-caption">{{formatDate(Task.DateAssigned)}}</span>
    <br/><br/>
    <md-layout :md-gutter="8">
      <md-layout md-flex="30" md-flex-small="100">
        <md-whiteframe md-tag="section" class="full-width padded">
          <div>
            <span class="md-display-1">Details</span>
            <div :class="type_class()" class="pull-right">
              <md-icon :class="type_animate()">{{type_icon()}}</md-icon>
            </div>
          </div>
          <md-list class="md-double-line expanded">
            <md-list-item>
              <div class="md-list-text-container">
                <span>{{Task.Description}}</span>
                <span>Description</span>
              </div>
            </md-list-item>
            <md-list-item>
              <div class="md-list-text-container">
                <span>{{formatDate(Task.DateDue)}}</span>
                <span>Due Date</span>
              </div>
            </md-list-item>
            <md-list-item>
              <div class="md-list-text-container">
                <div v-show="Task.AssignedTo.length">
                  <p class="no-margin" v-for="user in Task.AssignedTo">{{user.Email}}</p>
                </div>
                <div v-show="!Task.AssignedTo.length">
                  <span>Self</span>
                </div>
                <span>Assigned To</span>
              </div>
            </md-list-item>
            <md-list-item>
              <div class="md-list-text-container">
                <span>{{Task.Progress}}% Complete</span>
                <span>Progress</span>
              </div>
            </md-list-item>
            <md-list-item>
              <div class="md-list-text-container">
                <span>{{Task.Status.Title + ': ' + Task.Status.Description}}</span>
                <span>Status</span>
              </div>
            </md-list-item>
          </md-list>
        </md-whiteframe>
      </md-layout>
      <md-layout md-flex="40" md-flex-small="100">
        <md-whiteframe md-tag="section" class="full-width padded">
          <span class="md-display-1">Comments</span>
          <md-input-container>
            <label>Add Comment</label>
            <md-textarea></md-textarea>
          </md-input-container>
          <md-button class="md-raised md-accent pull-right no-margin" :disabled="log.loading">
            <md-icon>send</md-icon>
            Comment
          </md-button>
          <div style="width:100%;height: 340px; overflow-y: auto;margin-top: 100px;">
            <ul class="full-width comment-list chat">
              <li v-for="comment in log.content" :class="isSelf(comment.ApplicationUser.Id)">
                <p>{{comment.Description}}</p>
                <span>{{comment.ApplicationUser.UserName}}</span>
                <span>{{formatDate(comment.LogTime)}}</span>
              </li>
            </ul>
          </div>
        </md-whiteframe>
      </md-layout>
      <md-layout md-flex="30" md-flex-small="100">
        <md-whiteframe md-tag="section" class="full-width padded">
          <span class="md-display-1">Progress updates</span>
          <p>Following lists shows information about the progress updates.</p>
          <br/>
          <div style="width:100%;height: 430px; overflow-y: auto;">
            <ul class="full-width comment-list">
              <li v-for="comment in progressHistory.content">
                <md-progress :md-progress="comment.Progress"></md-progress>
                <span>{{comment.Progress}}% at {{formatDate(comment.UpdateTime)}}</span>
              </li>
            </ul>
          </div>
        </md-whiteframe>
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
          }, {
            "Claims": [],
            "Logins": [],
            "Roles": [],
            "FirstName": null,
            "LastName": null,
            "FullName": null,
            "Email": "tester2@test.com",
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
            "UserName": "tester2@test.com"
          }, {
            "Claims": [],
            "Logins": [],
            "Roles": [],
            "FirstName": null,
            "LastName": null,
            "FullName": null,
            "Email": "tester3@test.com",
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
            "UserName": "tester3@test.com"
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
    computed: {
      userinfo: function () {
        return this.$store.getters.getUserInfo || {};
      }
    },
    methods: {
      isSelf: function (userid) {
        return this.userinfo.ID == userid ? 'text-right' : 'text-left';
      },
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

        getLog(_self.Task.MainTaskID).then(function (res) {
          _self.$set(_self.log, 'content', res.data);
        }).catch(function (err) {
          console.dir(err);
        });
      },
      loadProgressHistory: function () {
        const _self = this;
        _self.$set(_self.progressHistory, 'loading', false);

        getProgressHistory(_self.Task.MainTaskID).then(function (res) {
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
        _self.$set(_self, 'Type', type || 'view');
        _self.$set(_self, 'Task', task);
      } else {
        _self.$router.push({
          path: '/tasks'
        });
      }
      */

      window.setTimeout(function () {

        _self.loadLog();
        _self.loadProgressHistory();
      }, 200);

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
    border-radius: 3px;
    margin: 5px;
    margin-left: 0;
    padding: 10px 5px;
  }
  
  .comment-list li.text-right {}
  
  .comment-list.chat li {
    background-color: rgba(233, 30, 99, 0.1);
  }
  
  .comment-list.chat li.text-right {
    background-color: transparent;
    background-color: rgba(0, 0, 0, 0.025);
  }
  
  .comment-list li.no-border {
    padding-bottom: 0;
  }
  
  .comment-list p {
    margin-top: 0;
    padding-top: 0;
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
  
  .padded {
    padding: 20px;
  }

</style>
