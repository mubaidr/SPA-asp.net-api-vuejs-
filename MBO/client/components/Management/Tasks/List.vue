<template>
  <div><span class="md-display-1">Archived Tasks</span>
    <p>
      <router-link class="md-accent" :to="{path: '/tasks/trash'}">
        View Trash
      </router-link>
    </p>
    <md-whiteframe md-tag="section" md-elevation="0">
      <md-tabs md-fixed>
        <md-tab :md-active="activeTab == TaskList.name" :md-label="TaskList.name" :md-icon="TaskList.icon" v-for="TaskList in Tasks">
          <pagination :lastpage="TaskList.last_page" :loading="TaskList.loading" :count="TaskList.count" @refresh="retry"></pagination>
          <md-layout md-gutter>
            <div class="flex-vertical min-height full-width" v-show="!TaskList.content.length">
              <div class="no-content">
                <md-icon class="md-accent md-size-2x" md-size-2x>cloud_queue</md-icon><br/>
                <span v-if="TaskList.loading">Loading...</span>
                <span v-else>Awww... Nothing here!</span>
                <span v-show="TaskList.error">An error occured while trying to fetch data.</span>
              </div>
            </div>
            <transition-group name="list-out" tag="ul" class="no-padding">
              <li class="list-out-item" v-for="Task in TaskList.content" v-bind:key="Task.MainTaskID">
                <task-card @remove-task-item="removeTaskItem" :Task="Task"></task-card>
              </li>
            </transition-group>
          </md-layout>
        </md-tab>
      </md-tabs>
      <md-snackbar md-position="bottom center" ref="snackbar" md-duration="60000">
        <span>Unable to fetch data!<br/> If the problem persists please contact support.</span>
        <md-button class="md-accent" @click="retry">Retry</md-button>
      </md-snackbar>
    </md-whiteframe>
  </div>
</template>
<script>
  import pagination from 'components/_custom/pagination.vue';
  import taskCard from 'components/_custom/task-card.vue';
  import {
    listAssigned,
    listCreated,
    listCompleted
  } from 'services/tasks';

  export default {
    name: 'task-list-all',
    components: {
      'task-card': taskCard,
      'pagination': pagination
    },
    data: function () {
      return {
        Tasks: {
          Assigned: {
            name: 'Assigned',
            icon: 'assignment_return',
            content: [],
            loading: true,
            last_page: 1,
            count: 0
          },
          Created: {
            name: 'Created',
            icon: 'assignment_returned',
            content: [],
            loading: true,
            last_page: 1,
            count: 0
          },
          Completed: {
            name: 'Completed',
            icon: 'assignment_turned_in',
            content: [],
            loading: true,
            last_page: 1,
            count: 0
          }
        },
        failAlert: false
      }
    },
    watch: {
      'Tasks.Assigned.content': function () {
        this.$set(this.Tasks.Assigned, 'loading', false);
      },
      'Tasks.Created.content': function () {
        this.$set(this.Tasks.Created, 'loading', false);
      },
      'Tasks.Completed.content': function () {
        this.$set(this.Tasks.Completed, 'loading', false);
      },
      'failAlert': function (val) {
        const _self = this;
        if (val) {
          _self.$refs.snackbar.open();
        } else {
          _self.$refs.snackbar.close();
        }
      }
    },
    computed: {
      activeTab: function () {
        const _self = this;
        const _path = _self.$route.query.sub;
        if (Object.keys(_self.Tasks).indexOf(_path) > -1) {
          return _path;
        } else {
          return 'Assigned';
        }
      },
      settings: function () {
        return this.$store.getters.getSettings;
      }
    },
    methods: {
      retry: function () {
        const _self = this;
        _self.$set(_self, 'failAlert', false);

        window.setTimeout(function () {
          _self.loadCompleted();
          _self.loadCreated();
          _self.loadAssigned();
        }, 500);
      },
      removeTaskItem: function (obj) {
        const _self = this;
        var id = obj.id;
        var type = obj.type;

        var ts = _self.Tasks[type].content;

        for (var i = 0; i < ts.length; i++) {
          if (ts[i].MainTaskID == id) {
            _self.Tasks[type].content.splice(i, 1);
            break;
          }
        }
      },
      loadAssigned: function () {
        const _self = this;
        _self.$set(_self.Tasks.Assigned, 'loading', true);
        listAssigned().then(res => {
          _self.$set(_self.Tasks.Assigned, 'content', res.data.mainTask);
          _self.$set(_self.Tasks.Assigned, 'last_page', res.data.last_page);
          _self.$set(_self.Tasks.Assigned, 'count', res.data.count);
        }).catch(err => {
          _self.$set(_self.Tasks.Assigned, 'loading', false);
          _self.$set(_self, 'failAlert', true);
        });
      },
      loadCompleted: function () {
        const _self = this;
        _self.$set(_self.Tasks.Completed, 'loading', true);
        listCompleted().then(res => {
          _self.$set(_self.Tasks.Completed, 'content', res.data.mainTask);
          _self.$set(_self.Tasks.Completed, 'last_page', res.data.last_page);
          _self.$set(_self.Tasks.Completed, 'count', res.data.count);
        }).catch(err => {
          _self.$set(_self.Tasks.Completed, 'loading', false);
          _self.$set(_self, 'failAlert', true);
        });
      },
      loadCreated: function () {
        const _self = this;
        _self.$set(_self.Tasks.Created, 'loading', true);
        listCreated().then(res => {
          _self.$set(_self.Tasks.Created, 'content', res.data.mainTask);
          _self.$set(_self.Tasks.Created, 'last_page', res.data.last_page);
          _self.$set(_self.Tasks.Created, 'count', res.data.count);
        }).catch(err => {
          _self.$set(_self.Tasks.Created, 'loading', false);
          _self.$set(_self, 'failAlert', true);
        });
      }
    },
    mounted: function () {
      const _self = this;

      window.setTimeout(function () {
        _self.loadCompleted();
        _self.loadCreated();
        _self.loadAssigned();
      }, 250);

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
