<template>
  <div>
    
    <md-layout :md-gutter="8">      
      <md-layout></md-layout>
      <md-layout md-flex="40" md-flex-small="100">
        <md-whiteframe md-tag="section" class="full-width padded">
          <span class="md-headline">{{Task.Title}}</span> by
          <span class="md-subheading">{{Task.AssignedBy.UserName}}</span><br/> on 
          <span class="md-caption">{{formatDate(Task.DateAssigned)}}</span>
          <p>
            <a href="#" @click.stop.prevent="goBack()" class="pull-right">Go Back</a>
          </p>
        </md-whiteframe>        
        <md-whiteframe md-tag="section" class="full-width padded">
          <span class="md-display-1">Comments</span>
          <md-input-container>
            <label>Add Comment</label>
            <md-textarea v-model="comment"></md-textarea>
          </md-input-container>
          <md-button class="md-raised md-accent pull-right no-margin" :disabled="log.loading" @click.native="addComment">
            <md-icon>send</md-icon>
            Comment
          </md-button>
          <div class="flex-vertical min-height full-width" v-show="!log.content.length">
            <div class="no-content">
              <md-icon class="md-accent md-size-2x" md-size-2x>cloud_queue</md-icon><br/>
              <span v-if="log.loading">Loading...</span>
              <span v-show="!log.content.length && !log.loading">Awww... Nothing here!</span>
            </div>
          </div>
          <div style="width:100%;height: auto; overflow-y: auto;margin-top: 100px;" v-show="log.content.length">
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
      <md-layout></md-layout>      
    </md-layout>
  </div>
</template>
<script>
  import {
    addLog,
    getLog
  } from 'services/taskLog'
  import moment from 'moment'

  export default {
    data () {
      return {
        Task: {
          'AssignedBy': {
            'Claims': [],
            'Logins': [],
            'Roles': [],
            'FirstName': null,
            'LastName': null,
            'FullName': null,
            'Email': 'tester@test.com',
            'EmailConfirmed': false,
            'PasswordHash': 'AAfNlYFPlbFENjLpOXQXzgm513SNYAoLZWlId5Md7j3lIFLOQaKZKNl0wk7O1lVkpA==',
            'SecurityStamp': '1292a83d-745b-4570-954b-015527ff1a42',
            'PhoneNumber': null,
            'PhoneNumberConfirmed': false,
            'TwoFactorEnabled': false,
            'LockoutEndDateUtc': null,
            'LockoutEnabled': false,
            'AccessFailedCount': 0,
            'Id': 'c9c03a40-d155-45cf-abd5-48f8cc4cf8e8',
            'UserName': 'tester@test.com'
          },
          'Category': {
            'CategoryID': 3,
            'Title': 'Placement',
            'Description': 'Placement'
          },
          'Status': {
            'StatusID': 1,
            'Title': 'Assigned',
            'Description': 'Task has been created',
            'Level': 0
          },
          'MainTaskID': 3,
          'Title': 'OOOOOOO',
          'Description': 'ssss',
          'IsDeleted': false,
          'Progress': 33,
          'DateAssigned': '2017-01-11T12:34:08.537',
          'DateDue': '2017-12-01T12:33:00',
          'StatusID': 1,
          'CategoryID': 3,
          'AssignedByID': 'c9c03a40-d155-45cf-abd5-48f8cc4cf8e8',
          'AssignedTo': [{
            'Claims': [],
            'Logins': [],
            'Roles': [],
            'FirstName': null,
            'LastName': null,
            'FullName': null,
            'Email': 'tester@test.com',
            'EmailConfirmed': false,
            'PasswordHash': 'AAfNlYFPlbFENjLpOXQXzgm513SNYAoLZWlId5Md7j3lIFLOQaKZKNl0wk7O1lVkpA===',
            'SecurityStamp': '1292a83d-745b-4570-954b-015527ff1a42',
            'PhoneNumber': null,
            'PhoneNumberConfirmed': false,
            'TwoFactorEnabled': false,
            'LockoutEndDateUtc': null,
            'LockoutEnabled': false,
            'AccessFailedCount': 0,
            'Id': 'c9c03a40-d155-45cf-abd5-48f8cc4cf8e8',
            'UserName': 'tester@test.com'
          }, {
            'Claims': [],
            'Logins': [],
            'Roles': [],
            'FirstName': null,
            'LastName': null,
            'FullName': null,
            'Email': 'tester2@test.com',
            'EmailConfirmed': false,
            'PasswordHash': 'AAfNlYFPlbFENjLpOXQXzgm513SNYAoLZWlId5Md7j3lIFLOQaKZKNl0wk7O1lVkpA===',
            'SecurityStamp': '1292a83d-745b-4570-954b-015527ff1a42',
            'PhoneNumber': null,
            'PhoneNumberConfirmed': false,
            'TwoFactorEnabled': false,
            'LockoutEndDateUtc': null,
            'LockoutEnabled': false,
            'AccessFailedCount': 0,
            'Id': 'c9c03a40-d155-45cf-abd5-48f8cc4cf8e8',
            'UserName': 'tester2@test.com'
          }, {
            'Claims': [],
            'Logins': [],
            'Roles': [],
            'FirstName': null,
            'LastName': null,
            'FullName': null,
            'Email': 'tester3@test.com',
            'EmailConfirmed': false,
            'PasswordHash': 'AAfNlYFPlbFENjLpOXQXzgm513SNYAoLZWlId5Md7j3lIFLOQaKZKNl0wk7O1lVkpA===',
            'SecurityStamp': '1292a83d-745b-4570-954b-015527ff1a42',
            'PhoneNumber': null,
            'PhoneNumberConfirmed': false,
            'TwoFactorEnabled': false,
            'LockoutEndDateUtc': null,
            'LockoutEnabled': false,
            'AccessFailedCount': 0,
            'Id': 'c9c03a40-d155-45cf-abd5-48f8cc4cf8e8',
            'UserName': 'tester3@test.com'
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
        },
        comment: 'Something to chear!'
      }
    },
    watch: {
      'log.content' () {
        this.$set(this.log, 'loading', false)
      },
      'progressHistory.content' () {
        this.$set(this.progressHistory, 'loading', false)
      }
    },
    computed: {
      userinfo () {
        return this.$store.getters.getUserInfo || {}
      }
    },
    methods: {
      addComment () {
        const _self = this
        const text = _self.comment
        if (text) {
          addLog({
            Description: text,
            MainTaskID: _self.Task.MainTaskID
          }).then(res => {
            _self.$set(_self.log, 'content', res.data)
          }).catch(err => {
            console.log(err)
          })
        } else {
          return false
        }
      },
      goBack () {
        this.$router.go(-1)
      },
      isSelf (userid) {
        return this.userinfo.ID === userid ? 'text-right' : 'text-left'
      },
      formatDate (date) {
        return moment(date).format('hh:mmA DD-MM-YY')
      },
      loadLog () {
        const _self = this
        _self.$set(_self.log, 'loading', false)

        getLog(_self.Task.MainTaskID).then(res => {
          _self.$set(_self.log, 'content', res.data)
        }).catch(err => {
          console.dir(err)
        })
      }
    },
    mounted () {
      const _self = this
      const task = _self.$route.params.Task
      const type = _self.$route.params.Type

      if (task) {
        _self.$set(_self, 'Type', type || 'view')
        _self.$set(_self, 'Task', task)
      } else {
        _self.$router.push({
          path: '/tasks'
        })
      }

      window.setTimeout(() => {
        _self.loadLog()
      }, 500)
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
  
  .theme-danger {
    color: #f44336;
  }
  
  .theme-warn {
    color: #ff5722;
  }
  
  .theme-normal {
    color: #ff9800;
  }
  
  .theme-primary {
    color: #cddc39;
  }
  
  .theme-success {
    color: #4caf50;
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
    border: 1px solid rgba(233, 30, 99, 0.08);
    background-color: rgba(233, 30, 99, 0.075);
  }
  
  .comment-list.chat li.text-right {
    border: 1px solid rgba(0, 0, 0, 0.05);
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
  

</style>
