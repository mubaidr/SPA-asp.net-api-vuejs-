<template>
  <div>
    <md-toolbar class="md-transparent">
      <md-button class="md-icon-button" @click.native="$router.go(-1)">
        <md-icon class="md-accent">arrow_back</md-icon>
      </md-button>
      <h2>Tasks</h2>
      <span style="flex: 1"></span>
      <md-button class="md-accent" @click.native="$router.push({path: '/dashboard'})">
        <md-tooltip md-direction="top">Dashboard</md-tooltip>
        <md-icon>dashboard</md-icon>
        Dashboard
      </md-button>
    </md-toolbar>
    <md-tabs md-fixed @change="tabChange" md-elevation="1">
      <md-tab v-for="item in list" :key="item.type" :md-active="currentTab === item.type" :md-label="item.type">
        <task-view :active="item.active" :type="item.type"></task-view>
      </md-tab>
    </md-tabs>
  </div>
</template>
<script>
import { find, startCase } from 'lodash'
import taskView from './_taskView.vue'

export default {
  components: {
    taskView
  },
  data () {
    return {
      currentTab: null,
      list: [{
        type: 'Created',
        active: false
      }, {
        type: 'Assigned',
        active: false
      }, {
        type: 'Completed',
        active: false
      }, {
        type: 'Archived',
        active: false
      }]
    }
  },
  watch: {},
  methods: {
    tabChange (index) {
      this.currentTab = this.list[index].type
      this.list[index].active = true
    },
    activeTab () {
      let _path = startCase(this.$route.query.sub)
      if (!find(this.list, { type: _path })) {
        _path = 'Assigned'
      }
      this.currentTab = _path
    }
  },
  created () {
    this.activeTab()
  }
}

</script>
<style>

</style>
