<template>
  <div>
    <!--<md-whiteframe md-tag="section" class="full-width">-->
    <md-toolbar class="md-transparent  md-dense">
      <md-button class="md-icon-button" @click.native="$router.push({ path: '/dashboard' })">
        <md-tooltip md-direction="top">Dashboard</md-tooltip>
        <md-icon class="md-accent">arrow_back</md-icon>
      </md-button>
      <span>All Tasks</span>
      <span style="flex: 1"></span>
      <md-button class="md-accent" @click.native="$router.push({path: '/tasks/trash'})">
        <md-tooltip md-direction="top">Archived</md-tooltip>
        <md-icon>delete</md-icon>
        Archived
      </md-button>
      <md-button class="md-accent" @click.native="$router.push({path: '/dashboard'})">
        <md-tooltip md-direction="top">Dashboard</md-tooltip>
        <md-icon>dashboard</md-icon>
        Dashboard
      </md-button>
    </md-toolbar>
  
    <!--</md-whiteframe>-->
    <br>
    <md-whiteframe md-tag="section" md-elevation="0">
      <md-tabs md-fixed @change="tabChange" md-elevation="2">
        <md-tab :md-active="currentTab === TaskList.name" :md-label="TaskList.name" :md-icon="TaskList.icon" v-for="TaskList in Tasks" :key="TaskList.name">
          <pagination :lastpage="TaskList.lastPage" :loading="TaskList.loading" :count="TaskList.count" :view-menu="true" :compact="true" :full-width="true" @refresh="search"></pagination>
          <md-layout md-gutter="16">
            <md-layout v-show="activeView == 'List'" md-hide-small></md-layout>
            <md-layout md-gutter="16">
              <transition-group class="min-height no-padding full-width simple-list" name="list-out" appear tag="ul" v-if="TaskList.content.length">
                <li class="list-out-item min-width" :class="{'full-width' : activeView == 'List'}" v-for="Task in TaskList.content" v-bind:key="Task.MainTaskID">
                  <task-card @remove-task-item="removeTaskItem" :Task="Task" :Type="TaskList.name" v-if="activeView == 'Card'"></task-card>
                  <task-list-item @remove-task-item="removeTaskItem" :Task="Task" :Type="TaskList.name" v-else></task-list-item>
                </li>
              </transition-group>
              <data-state :loading="TaskList.loading" :error="TaskList.error" v-else></data-state>
            </md-layout>
            <md-layout v-show="activeView == 'List'" md-hide-small></md-layout>
          </md-layout>
        </md-tab>
      </md-tabs>
      <md-snackbar md-position="bottom center" ref="snackbar" md-duration="60000">
        <span>Unable to fetch data!
          <br> If the problem persists please contact support.</span>
        <md-button class="md-accent" @click.native="retry">Retry</md-button>
      </md-snackbar>
    </md-whiteframe>
  </div>
</template>
<script>
  import pagination from 'components/_custom/pagination.vue'
  import taskCard from 'components/_custom/task-card.vue'
  import taskListItem from 'components/_custom/task-list-item.vue'
  import dataState from 'components/_custom/data-state.vue'
  import {
    listAssigned,
    listCreated,
    listCompleted
  } from 'services/tasks'

  export default {
    name: 'task-list-all',
    components: {
      taskCard,
      taskListItem,
      pagination,
      dataState
    },
    data () {
      return {
        Tasks: {
          Assigned: {
            name: 'Assigned',
            icon: 'assignment_return',
            content: [],
            loading: true,
            lastPage: 1,
            count: 0
          },
          Created: {
            name: 'Created',
            icon: 'assignment_returned',
            content: [],
            loading: true,
            lastPage: 1,
            count: 0
          },
          Completed: {
            name: 'Completed',
            icon: 'assignment_turned_in',
            content: [],
            loading: true,
            lastPage: 1,
            count: 0
          }
        },
        currentTab: 'Assigned',
        failAlert: false
      }
    },
    watch: {
      'Tasks.Assigned.content' () {
        this.Tasks.Assigned.loading = false
      },
      'Tasks.Created.content' () {
        this.Tasks.Created.loading = false
      },
      'Tasks.Completed.content' () {
        this.Tasks.Completed.loading = false
      },
      'failAlert' (val) {
        if (val) {
          this.$refs.snackbar.open()
        } else {
          this.$refs.snackbar.close()
        }
      }
    },
    computed: {
      settings () {
        return this.$store.getters.getSettings
      },
      activeView () {
        for (var i = 0; i < this.settings.taskView.view.length; i++) {
          if (this.settings.taskView.view[i].enabled) {
            return this.settings.taskView.view[i].name
          }
        }
      }
    },
    methods: {
      tabChange (index) {
        this.currentTab = Object.keys(this.Tasks)[index]

        switch (this.currentTab) {
          case 'Assigned':
            if (!this.Tasks[this.currentTab].content.length) {
              this.loadAssigned()
            }
            break
          case 'Created':
            if (!this.Tasks[this.currentTab].content.length) {
              this.loadCreated()
            }
            break
          case 'Completed':
            if (!this.Tasks[this.currentTab].content.length) {
              this.loadCompleted()
            }
            break
        }
      },
      activeTab () {
        let _path = this.$route.query.sub
        if (!Object.keys(this.Tasks).includes(_path)) {
          _path = 'Assigned'
        }
        this.currentTab = _path
      },
      search (obj) {
        switch (this.currentTab) {
          case 'Assigned':
            this.loadAssigned(obj)
            break
          case 'Created':
            this.loadCreated(obj)
            break
          case 'Completed':
            this.loadCompleted(obj)
            break
        }
      },
      retry () {
        this.failAlert = false

        window.setTimeout(() => {
          this.loadCompleted()
          this.loadCreated()
          this.loadAssigned()
        }, 500)
      },
      removeTaskItem (obj) {
        const id = obj.id
        const type = obj.type

        const ts = this.Tasks[type].content

        for (let i = 0; i < ts.length; i++) {
          if (ts[i].MainTaskID === id) {
            this.Tasks[type].content.splice(i, 1)
            break
          }
        }

        window.setTimeout(this.search, 250)
      },
      loadAssigned (obj) {
        this.Tasks.Assigned.loading = true

        listAssigned(obj).then(res => {
          this.Tasks.Assigned.content = res.data.mainTask
          this.Tasks.Assigned.lastPage = res.data.last_page
          this.Tasks.Assigned.count = res.data.count
        }).catch(err => {
          this.setErrorDetails(err)
        }).then(() => {
          this.Tasks.Assigned.loading = false
        })
      },
      loadCompleted (obj) {
        this.Tasks.Completed.loading = true

        listCompleted(obj).then(res => {
          this.Tasks.Completed.content = res.data.mainTask
          this.Tasks.Completed.lastPage = res.data.last_page
          this.Tasks.Completed.count = res.data.count
        }).catch(err => {
          this.setErrorDetails(err)
        }).then(() => {
          this.Tasks.Completed.loading = false
        })
      },
      loadCreated (obj) {
        this.Tasks.Created.loading = true

        listCreated(obj).then(res => {
          this.Tasks.Created.content = res.data.mainTask
          this.Tasks.Created.lastPage = res.data.last_page
          this.Tasks.Created.count = res.data.count
        }).catch(err => {
          this.setErrorDetails(err)
        }).then(() => {
          this.Tasks.Created.loading = false
        })
      }
    },
    mounted () {
      this.activeTab()

      window.setTimeout(() => {
        this.search()
      }, 250)
    }
  }

</script>
<style>
  
</style>
