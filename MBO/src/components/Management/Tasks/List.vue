<template>
  <div>
    <md-whiteframe md-tag="section" class="full-width">
      <md-toolbar>
        <md-button class="md-icon-button" @click.native="$router.push({ path: '/dashboard' })">
          <md-tooltip md-direction="top">Dashboard</md-tooltip>
          <md-icon>arrow_back</md-icon>
        </md-button>
        <span class="md-headline">Tasks</span>
        <span style="flex: 1"></span>
        <md-button @click.native="$router.push({path: '/tasks/trash'})">
          <md-tooltip md-direction="top">Archived</md-tooltip>
          Archived
        </md-button>
      </md-toolbar>      
    </md-whiteframe>
    <br/>
    <md-whiteframe md-tag="section" md-elevation="0">
      <md-tabs md-fixed @change="tabChange" md-elevation="2">
        <md-tab :md-active="currentTab === TaskList.name" :md-label="TaskList.name" :md-icon="TaskList.icon" v-for="TaskList in Tasks">
          <pagination :lastpage="TaskList.lastPage" :loading="TaskList.loading" :count="TaskList.count" @refresh="search"></pagination>
          <md-layout md-gutter>
            <transition-group name="list-out" tag="ul" class="min-height no-padding">
              <li class="list-out-item" v-for="Task in TaskList.content" v-bind:key="Task.MainTaskID">
                <task-card @remove-task-item="removeTaskItem" :Task="Task" :Type="TaskList.name"></task-card>
              </li>
            </transition-group>
            <div class="flex-vertical min-height full-width" v-show="!TaskList.content.length">
              <div class="no-content">
                <md-icon class="md-accent md-size-2x" md-size-2x>cloud_queue</md-icon><br/>
                <span v-if="TaskList.loading">Loading...</span>
                <span v-else>Awww... Nothing here!</span>
                <span v-show="TaskList.error">An error occured while trying to fetch data.</span>
              </div>
            </div>
          </md-layout>
        </md-tab>
      </md-tabs>
      <md-snackbar md-position="bottom center" ref="snackbar" md-duration="60000">
        <span>Unable to fetch data!<br/> If the problem persists please contact support.</span>
        <md-button class="md-accent" @click.native="retry">Retry</md-button>
      </md-snackbar>
    </md-whiteframe>
  </div>
</template>
<script>
  import pagination from 'components/_custom/pagination.vue'
  import taskCard from 'components/_custom/task-card.vue'
  import {
    listAssigned,
    listCreated,
    listCompleted
  } from 'services/tasks'

  export default {
    name: 'task-list-all',
    components: {
      'task-card': taskCard,
      'pagination': pagination
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
        this.$set(this.Tasks.Assigned, 'loading', false)
      },
      'Tasks.Created.content' () {
        this.$set(this.Tasks.Created, 'loading', false)
      },
      'Tasks.Completed.content' () {
        this.$set(this.Tasks.Completed, 'loading', false)
      },
      'failAlert' (val) {
        const _self = this
        if (val) {
          _self.$refs.snackbar.open()
        } else {
          _self.$refs.snackbar.close()
        }
      }
    },
    computed: {
      settings () {
        return this.$store.getters.getSettings
      }
    },
    methods: {
      tabChange (index) {
        const _self = this
        _self.$set(_self, 'currentTab', Object.keys(_self.Tasks)[index])

        switch (_self.currentTab) {
          case 'Assigned':
            if (!_self.Tasks[_self.currentTab].content.length) {
              _self.loadAssigned()
            }
            break
          case 'Created':
            if (!_self.Tasks[_self.currentTab].content.length) {
              _self.loadCreated()
            }
            break
          case 'Completed':
            if (!_self.Tasks[_self.currentTab].content.length) {
              _self.loadCompleted()
            }
            break
        }
      },
      activeTab () {
        const _self = this
        let _path = _self.$route.query.sub
        if (!Object.keys(_self.Tasks).includes(_path)) {
          _path = 'Assigned'
        }
        _self.$set(_self, 'currentTab', _path)
      },
      search (obj) {
        const _self = this

        switch (_self.currentTab) {
          case 'Assigned':
            _self.loadAssigned(obj)
            break
          case 'Created':
            _self.loadCreated(obj)
            break
          case 'Completed':
            _self.loadCompleted(obj)
            break
        }
      },
      retry () {
        const _self = this
        _self.$set(_self, 'failAlert', false)

        window.setTimeout(() => {
          _self.loadCompleted()
          _self.loadCreated()
          _self.loadAssigned()
        }, 500)
      },
      removeTaskItem (obj) {
        const _self = this
        const id = obj.id
        const type = obj.type

        const ts = _self.Tasks[type].content

        for (let i = 0; i < ts.length; i++) {
          if (ts[i].MainTaskID === id) {
            _self.Tasks[type].content.splice(i, 1)
            break
          }
        }

        window.setTimeout(_self.search, 250)
      },
      loadAssigned (obj) {
        const _self = this
        _self.$set(_self.Tasks.Assigned, 'loading', true)
        listAssigned(obj).then(res => {
          _self.$set(_self.Tasks.Assigned, 'content', res.data.mainTask)
          _self.$set(_self.Tasks.Assigned, 'lastPage', res.data.last_page)
          _self.$set(_self.Tasks.Assigned, 'count', res.data.count)
        }).catch(err => {
          _self.$set(_self.Tasks.Assigned, 'loading', false)
          _self.$set(_self, 'failAlert', true)
          console.log(err)
        })
      },
      loadCompleted (obj) {
        const _self = this
        _self.$set(_self.Tasks.Completed, 'loading', true)
        listCompleted(obj).then(res => {
          _self.$set(_self.Tasks.Completed, 'content', res.data.mainTask)
          _self.$set(_self.Tasks.Completed, 'lastPage', res.data.last_page)
          _self.$set(_self.Tasks.Completed, 'count', res.data.count)
        }).catch(err => {
          _self.$set(_self.Tasks.Completed, 'loading', false)
          _self.$set(_self, 'failAlert', true)
          console.log(err)
        })
      },
      loadCreated (obj) {
        const _self = this
        _self.$set(_self.Tasks.Created, 'loading', true)
        listCreated(obj).then(res => {
          _self.$set(_self.Tasks.Created, 'content', res.data.mainTask)
          _self.$set(_self.Tasks.Created, 'lastPage', res.data.last_page)
          _self.$set(_self.Tasks.Created, 'count', res.data.count)
        }).catch(err => {
          _self.$set(_self.Tasks.Created, 'loading', false)
          _self.$set(_self, 'failAlert', true)
          console.log(err)
        })
      }
    },
    mounted () {
      const _self = this
      _self.activeTab()

      window.setTimeout(() => {
        _self.search()
      }, 250)
    }
  }

</script>
<style scoped>
  .no-content {
    text-align: center;
  }
  
  .no-content span {
    font-size: 1.25em;
    opacity: 0.75;
  }
  
  .no-content i {
    margin-bottom: 10px;
  }
  
  .md-tab {
    padding: 16px 2px;
  }

</style>
