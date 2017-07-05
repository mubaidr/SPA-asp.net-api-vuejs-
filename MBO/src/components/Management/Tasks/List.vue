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
      <md-tab v-for="item in list" :key="item.label" :md-active="currentTab === item.label" :md-label="item.label">
        <created :loaded="item.loaded"></created>
      </md-tab>
    </md-tabs>
  </div>
</template>
<script>
import { find, startCase } from 'lodash'
import created from './_created.vue'
import assigned from './_assigned.vue'
import completed from './_completed.vue'
import archived from './_archived.vue'

export default {
  components: {
    created,
    assigned,
    completed,
    archived
  },
  data () {
    return {
      currentTab: null,
      list: [{
        label: 'Created',
        loaded: false
      }, {
        label: 'Assigned',
        loaded: false
      }, {
        label: 'Completed',
        loaded: false
      }, {
        label: 'Archived',
        loaded: false
      }]
    }
  },
  watch: {
    'currentTab' (tab) {
      this.list[tab].loaded = true
    }
  },
  methods: {
    tabChange (index) {
      this.currentTab = this.list[index].label
    },
    activeTab () {
      let _path = startCase(this.$route.query.sub)
      if (!find(this.list, { label: _path })) {
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
