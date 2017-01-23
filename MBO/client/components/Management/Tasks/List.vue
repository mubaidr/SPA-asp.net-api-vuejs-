<template>
  <div>
    <md-whiteframe md-tag="section" md-elevation="0">
      <md-toolbar class="md-transparent">
        <router-link tag="md-button" to="/tasks/create" class="md-accent">
          <md-icon>add</md-icon>
          Add Task
        </router-link>
        <span style="flex: 1"></span>
        <md-button class="md-icon-button">
          <md-icon>search</md-icon>
        </md-button>
        <!--View change-->
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
        <!--<md-menu md-direction="bottom left" md-size="3">
        <md-button md-menu-trigger class="md-icon-button">
          <md-icon>dashboard</md-icon>
        </md-button>
        <md-menu-content>
          <md-menu-item v-for="type in settings.task_view.type" :disabled="type.enabled">
            <span>{{type.name}}</span>
            <md-icon>{{type.icon}}</md-icon>
          </md-menu-item>
        </md-menu-content>
      </md-menu>-->
      </md-toolbar>
    </md-whiteframe>
    <md-whiteframe md-tag="section" md-elevation="0">
      <md-tabs md-fixed>
        <md-tab :md-label="TaskList.name" :md-icon="TaskList.icon" v-for="TaskList in Tasks">
          <md-layout md-gutter>
            <div class="flex-vertical min-height full-width" v-show="!TaskList.prop.content.length">
              <md-spinner md-indeterminate class="md-accent" v-show="TaskList.prop.loading"></md-spinner>
              <p v-show="!TaskList.prop.loading" class="no-content">
                <md-icon class="md-accent md-size-3x" md-size-3x>cloud_queue</md-icon><br/>
                <span>{{TaskList.message || "Nothing here!"}} <span v-show="TaskList.error">An error occured while trying to fetch data.</span></span>
              </p>
            </div>
            <task-card v-for="Task in TaskList.prop.content" :Task="Task"></task-card>
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
    getUsersList
  } from 'services/account'
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
            message: '',
            prop: {
              content: [],
              loading: true,
              error: false
            }
          },
          Created: {
            name: 'Created',
            icon: 'assignment_returned',
            message: '',
            prop: {
              content: [],
              loading: true,
              error: false
            }
          },
          Completed: {
            name: 'Completed',
            icon: 'assignment_turned_in',
            message: '',
            prop: {
              content: [],
              loading: true,
              error: false
            }
          }
        },
        Catalog: {
          Categories: []
        }
      }
    },
    computed: {
      settings: function () {
        return this.$store.getters.getSettings;
      }
    },
    methods: {
      openDialog(ref) {
        this.$refs[ref].open();
      },
      closeDialog(ref) {
        this.$refs[ref].close();
      }
    },
    mounted: function () {
      const _self = this;

      listAssigned().then(res => {
        console.dir(res.data[0]);
        _self.$set(_self.Tasks.Assigned, 'prop', {
          content: res.data,
          loading: false,
          error: false
        });
      }).catch(err => {
        _self.$set(_self.Tasks.Assigned, 'prop', {
          content: [],
          loading: false,
          error: true
        });
      });

      listCreated().then(res => {
        _self.$set(_self.Tasks.Created, 'prop', {
          content: res.data,
          loading: false,
          error: false
        });
      }).catch(err => {
        _self.$set(_self.Tasks.Created, 'prop', {
          content: [],
          loading: false,
          error: true
        });
      });

      listCompleted().then(res => {
        _self.$set(_self.Tasks.Completed, 'prop', {
          content: res.data,
          loading: false,
          error: false
        });
      }).catch(err => {
        _self.$set(_self.Tasks.Completed, 'prop', {
          content: [],
          loading: false,
          error: true
        });
      });

      getCategories().then(res => {
        _self.$set(_self.Catalog, 'Categories', res.data);
      }).catch(err => {});

    }
  }

</script>
<style scoped>
  .bg-white {
    background-color: #fff;
  }
  
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
    min-height: 480px;
    padding: 16px 2px;
  }

</style>
