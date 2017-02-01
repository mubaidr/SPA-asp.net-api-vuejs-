<template>
  <div>
    <md-whiteframe md-tag="section" md-elevation="0">
      <span class="md-display-1">Tasks</span>
      <md-toolbar class="md-transparent no-padding">
        <router-link to="/tasks/trash" class="md-warn" id="btn-view-trash">
          <md-icon>archive</md-icon>
          Archived
        </router-link>
        <span style="flex: 1"></span>
        <md-button class="md-icon-button">
          <md-icon>search</md-icon>
        </md-button>
        <md-menu md-direction="bottom left" md-size="3">
          <md-button md-menu-trigger class="md-icon-button">
            <md-icon>filter_list</md-icon>
          </md-button>
          <md-menu-content>
            <md-menu-item disabled>Filter By</md-menu-item>
            <md-menu-item v-for="filter in settings.task_view.filter" :disabled="filter.enabled">
              <span>{{filter.name}} {{filter.type}}</span>
              <md-icon>{{filter.icon}}</md-icon>
            </md-menu-item>
          </md-menu-content>
        </md-menu>
        <md-menu md-direction="bottom left" md-size="3">
          <md-button md-menu-trigger class="md-icon-button">
            <md-icon>sort</md-icon>
          </md-button>
          <md-menu-content>
            <md-menu-item disabled>Sort By</md-menu-item>
            <md-menu-item v-for="sort in settings.task_view.sort" :disabled="sort.enabled">
              <span>{{sort.name}} {{sort.type}}</span>
              <md-icon>{{sort.icon}}</md-icon>
            </md-menu-item>
          </md-menu-content>
        </md-menu>
      </md-toolbar>
    </md-whiteframe>
    <md-whiteframe md-tag="section" md-elevation="0">
      <md-tabs md-fixed>
        <md-tab :md-active="activeTab == TaskList.name" :md-label="TaskList.name" :md-icon="TaskList.icon" v-for="TaskList in Tasks">
          <md-layout md-gutter>
            <div class="flex-vertical min-height full-width" v-show="!TaskList.content.length">
              <p v-show="TaskList.loading" class="no-content">
                <md-spinner md-indeterminate class="md-accent"></md-spinner><br/>
                <span>Fetching data!</span>
              </p>
              <p v-show="!TaskList.loading" class="no-content">
                <md-icon class="md-accent md-size-2x" md-size-2x>cloud_queue</md-icon><br/>
                <span>{{TaskList.message || "Awww... Nothing here!"}} <span v-show="TaskList.error">An error occured while trying to fetch data.</span></span>
              </p>
            </div>
            <transition-group name="list-in" tag="ul" class="no-padding">
              <li class="list-in-item" v-for="Task in TaskList.content" v-bind:key="Task.MainTaskID">
                <task-card @remove-task-item="removeTaskItem" :Task="Task" :Type="TaskList.name"></task-card>
              </li>
            </transition-group>
          </md-layout>
        </md-tab>
      </md-tabs>
    </md-whiteframe>
  </div>
</template>
<script>
  import taskCreate from 'components/Management/Tasks/_partial_create.vue'
  import taskCard from 'components/_custom/task-card.vue';
  import {
    listAssigned,
    listCreated,
    listCompleted
  } from 'services/tasks';

  import {
    getCategories
  } from 'services/catalogs';

  export default {
    name: 'task-list',
    components: {
      'task-card': taskCard,
      'task-create': taskCreate
    },
    data: function () {
      return {
        Tasks: {
          Assigned: {
            name: 'Assigned',
            icon: 'assignment_return',
            content: [],
            loading: true
          },
          Created: {
            name: 'Created',
            icon: 'assignment_returned',
            content: [],
            loading: true
          },
          Completed: {
            name: 'Completed',
            icon: 'assignment_turned_in',
            content: [],
            loading: true
          }
        },
        Catalog: {
          Categories: []
        }
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
        listAssigned().then(res => {
          _self.$set(_self.Tasks.Assigned, 'content', res.data);
        }).catch(err => {});
      },
      loadCompleted: function () {
        const _self = this;
        listCompleted().then(res => {
          _self.$set(_self.Tasks.Completed, 'content', res.data);
        }).catch(err => {});
      },
      loadCreated: function () {
        const _self = this;
        listCreated().then(res => {
          _self.$set(_self.Tasks.Created, 'content', res.data);
        }).catch(err => {});
      }
    },
    mounted: function () {
      const _self = this;

      window.setTimeout(function () {
        _self.loadCompleted();
        _self.loadCreated();
        _self.loadAssigned();
      }, 250);

      getCategories().then(res => {
        _self.$set(_self.Catalog, 'Categories', res.data);
      }).catch(err => {});
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
