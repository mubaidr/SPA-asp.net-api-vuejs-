<template>
  <div>

    <md-layout :md-gutter="8">
      <md-layout></md-layout>
      <md-layout md-flex="40" md-flex-small="100">
        <md-whiteframe class="full-width padded" md-tag="section">
          <span class="md-headline">{{Task.Title}}</span> by
          <span class="md-subheading">{{Task.AssignedBy.UserName}}</span><br> on
          <span class="md-caption">{{formatDate(Task.DateAssigned)}}</span>
          <p>
            <a class="pull-right" href="#" @click.stop.prevent="goBack()">Go Back</a>
          </p>
        </md-whiteframe>
        <md-whiteframe class="full-width padded" md-tag="section">
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
              <md-icon class="md-accent md-size-2x" md-size-2x>cloud_queue</md-icon><br>
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
        Task: {},
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
        this.log.loading = false
      },
      'progressHistory.content' () {
        this.progressHistory.loading = false
      }
    },
    computed: {
      userinfo () {
        return this.$store.getters.getUserInfo || {}
      }
    },
    methods: {
      addComment () {
        const text = this.comment
        if (text) {
          addLog({
            Description: text,
            MainTaskID: this.Task.MainTaskID
          }).then(res => {
            this.log.content = res.data
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
        this.log.loading = true

        getLog(this.Task.MainTaskID).then(res => {
          this.log.content = res.data
        }).catch(err => {
          console.dir(err)
        }).then(() => {
          this.log.loading = false
        })
      }
    },
    mounted () {
      const task = this.$route.params.Task
      const type = this.$route.params.Type

      if (task) {
        this.Type = type || 'view'
        this.Task = task
      } else {
        this.$router.push({
          path: '/tasks'
        })
      }

      window.setTimeout(() => {
        this.loadLog()
      }, 500)
    }
  }

</script>
<style scoped="">
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
