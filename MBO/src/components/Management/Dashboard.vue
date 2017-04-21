<template>
  <div>
    <md-layout md-gutter="40">
      <md-layout md-flex-small="50" md-flex-xlarge="25">
        <span class="counter">
          <span class="counter-up" :data-total="dashboard.TasksCreatedCount">0</span>
        <br/>
        <span style="font-size: small; opacity: 0.75;">Created</span>
        </span>
      </md-layout>
      <md-layout md-flex-small="50" md-flex-xlarge="25">
        <span class="counter">
          <span class="counter-up" :data-total="dashboard.TasksAssignedCount">0</span>
        <br/>
        <span style="font-size: small; opacity: 0.75;">Assigned</span></span>
      </md-layout>
      <md-layout md-flex-small="50" md-flex-xlarge="25">
        <span class="counter">
          <span class="counter-up" :data-total="dashboard.TasksCompletedCount">0</span>
        <br/>
        <span style="font-size: small; opacity: 0.75;">Completed</span></span>
      </md-layout>
      <md-layout md-flex-small="50" md-flex-xlarge="25">
        <span class="counter">
          <span class="counter-up" :data-total="dashboard.LogsCount">0</span>
        <br/>
        <span style="font-size: small; opacity: 0.75;">Comments</span></span>
      </md-layout>
    </md-layout>
    <br/>
    <md-layout md-gutter="40">
      <md-layout md-flex-xsmall="100" md-flex-small="70" md-flex-medium="66" md-flex-large="80" md-flex-xlarge="80">
        <md-layout md-gutter>
          <bar-chart :options="{responsive: true, maintainAspectRatio: true}" class="chart-full-width"></bar-chart>
        </md-layout>
      </md-layout>
      <md-layout md-flex-xsmall="100" md-flex-small="30" md-flex-medium="33" md-flex-large="20" md-flex-xlarge="20">
        <md-list class="full-width">
          <md-subheader class="md-inset">My Tasks</md-subheader>
          <md-list-item>
            <md-icon>add</md-icon>
            <span><router-link to="/tasks/create">Create</router-link></span>
          </md-list-item>
          <md-list-item>
            <md-icon>assignment_return</md-icon>
            <span>
              <router-link :to="{path: '/tasks', query: {sub: 'Assigned'}}">Assigned to me <md-chip>{{dashboard.TasksAssignedCount}}</md-chip></router-link>
            </span>
          </md-list-item>
          <md-list-item>
            <md-icon>assignment</md-icon>
            <span><router-link :to="{path: '/tasks', query: {sub: 'Created'}}">Created by me <md-chip>{{dashboard.TasksCreatedCount}}</md-chip></router-link>
            </span>
          </md-list-item>
          <md-list-item>
            <md-icon>assignment_turned_in</md-icon>
            <span><router-link :to="{path: '/tasks', query: {sub: 'Completed'}}">Completed <md-chip>{{dashboard.TasksCompletedCount}}</md-chip></router-link>
            </span>
          </md-list-item>
          <md-list-item>
            <md-icon>delete</md-icon>
            <span><router-link to="/tasks/trash">Trash <md-chip>{{dashboard.TasksDeletedCount}}</md-chip></router-link>
            </span>
          </md-list-item>
          <md-subheader class="md-inset">Messages</md-subheader>
          <md-list-item>
            <md-icon>mail</md-icon> <span><router-link :to="{path: '/Messages'}">Inbox <md-chip>{{dashboard.MessagesReceivedCount}}</md-chip></router-link></span>
          </md-list-item>
          <md-list-item>
            <md-icon>delete</md-icon> <span><router-link :to="{path: '/Messages/Trash'}">Trash <md-chip>{{dashboard.MessagesDeletedCount}}</md-chip></router-link></span>
          </md-list-item>
        </md-list>
      </md-layout>
    </md-layout>
  </div>
</template>
<script>
  import CountUp from 'countup.js'
  import ChartTasks from 'components/_charts/tasks.js'
  import {
    getDashboard
  } from 'services/dashboard'

  export default {
    data () {
      return {
        dashboard: {}
      }
    },
    components: {
      'bar-chart': ChartTasks
    },
    created () {
      var _self = this

      getDashboard().then(res => {
        _self.dashboard = res.data

        window.setTimeout(function () {
          var options = {
            useEasing: true,
            separator: ','
          }

          var els = document.getElementsByClassName('counter-up')
          for (var i = 0; i <
            els.length; i++) {
            var countup = new CountUp(els[i], 0, els[i].getAttribute('data-total'), 0, 3, options)
            countup.start()
          }
        }, 500)
      }).catch(err => { console.log(err) })
    },
    mounted () {

    }
  }

</script>
<style scoped>
  .counter {
    width: 50%;
    margin: 5px auto;
    text-align: center;
    padding: 10% 5%;
    font-size: 50px;
    background-color: #3f51b5;
    color: #fff;
    text-shadow: 0 0 3px black;
  }
  
  a .md-chip {
    float: right;
  }
  
  .chart-full-width {
    width: 100%;
  }
  
  .chart-full-width canvas {
    width: 100%;
  }

</style>
