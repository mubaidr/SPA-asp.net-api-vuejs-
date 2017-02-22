<template>
  <div>
    <md-layout md-gutter="40">
      <md-layout md-flex-small="50" md-flex-xlarge="25">
        <md-list class="full-width">
          <md-subheader class="md-inset">Tasks Created</md-subheader>
          <md-list-item>
            <md-icon>assignment</md-icon>
            <!--<vuecountup :end="2500" :duration="5" :options="{useEasing : true, useGrouping : true, separator : ',', decimal : '.', prefix : '', suffix : ''}"></vuecountup>-->
          </md-list-item>
        </md-list>
      </md-layout>
      <md-layout md-flex-small="50" md-flex-xlarge="25">
        <md-list class="full-width">
          <md-subheader class="md-inset">Tasks Assigned</md-subheader>
          <md-list-item>
            <md-icon>assignment_return</md-icon>
            <span>{{dashboard.TasksAssignedCount}}</span>
          </md-list-item>
        </md-list>
      </md-layout>
      <md-layout md-flex-small="50" md-flex-xlarge="25">
        <md-list class="full-width">
          <md-subheader class="md-inset">Tasks Completed</md-subheader>
          <md-list-item>
            <md-icon>assignment_turned_in</md-icon>
            <span>{{dashboard.TasksCompletedCount}}</span>
          </md-list-item>
        </md-list>
      </md-layout>
      <md-layout md-flex-small="50" md-flex-xlarge="25">
        <md-list class="full-width">
          <md-subheader class="md-inset">Comments Posted</md-subheader>
          <md-list-item>
            <md-icon>chat</md-icon>
            <span>{{dashboard.LogsCount}}</span>
          </md-list-item>
        </md-list>
      </md-layout>
    </md-layout>
    <br/>
    <md-layout md-gutter="40">
      <md-layout md-flex-xsmall="100" md-flex-small="70" md-flex-medium="66" md-flex-large="75" md-flex-xlarge="75">
        <span class="md-display-1">Graph Here</span>
      </md-layout>
      <md-layout md-flex-xsmall="100" md-flex-small="30" md-flex-medium="33" md-flex-large="25" md-flex-xlarge="25">
        <md-list class="full-width">
          <md-subheader class="md-inset">My Tasks</md-subheader>
          <md-list-item>
            <md-icon>add</md-icon>
            <span><router-link to="/tasks/create">Create</router-link></span>
          </md-list-item>
          <md-list-item>
            <md-icon>assignment_return</md-icon>
            <span><router-link :to="{path: '/tasks', query: {sub: 'Assigned'}}">Assigned to me <span class="md-caption">({{dashboard.TasksAssignedCount}})</span></router-link></span>
          </md-list-item>
          <md-list-item>
            <md-icon>assignment</md-icon>
            <span><router-link :to="{path: '/tasks', query: {sub: 'Created'}}">Created by me <span class="md-caption">({{dashboard.TasksCreatedCount}})</span></router-link></span>
          </md-list-item>
          <md-list-item>
            <md-icon>assignment_turned_in</md-icon>
            <span><router-link :to="{path: '/tasks', query: {sub: 'Completed'}}">Completed <span class="md-caption">({{dashboard.TasksCompletedCount}})</span></router-link></span>
          </md-list-item>
          <md-list-item>
            <md-icon>delete</md-icon>
            <span><router-link to="/tasks/trash">Trash <span class="md-caption">({{dashboard.TasksDeletedCount}})</span></router-link></span>
          </md-list-item>
          <md-subheader class="md-inset">Messages</md-subheader>
          <md-list-item>
            <md-icon>add</md-icon> <span>Compose</span>
          </md-list-item>
          <md-list-item>
            <md-icon>mail</md-icon> <span>Inbox <span class="md-caption">({{dashboard.MessagesReceivedCount}})</span></span>
          </md-list-item>
          <md-list-item>
            <md-icon>send</md-icon> <span>Outbox <span class="md-caption">({{dashboard.MessagesSentCount}})</span></span>
          </md-list-item>
          <md-list-item>
            <md-icon>delete</md-icon> <span>Trash <span class="md-caption">({{dashboard.MessagesDeletedCount}})</span></span>
          </md-list-item>
        </md-list>
      </md-layout>
    </md-layout>
  </div>
</template>
<script>
  import {
    getDashboard
  } from 'services/dashboard'

  export default {
    data () {
      return {
        dashboard: {}
      }
    },
    mounted () {
      getDashboard().then(res => {
        this.$set(this, 'dashboard', res.data)
        console.log(res)
      }).catch(err => {
        console.log(err)
      })
    }
  }

</script>
