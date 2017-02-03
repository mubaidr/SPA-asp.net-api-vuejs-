<template>
  <div><span class="md-display-1">Archived Tasks</span>
    <br/>
    <router-link class="md-accent" :to="{path: '/tasks'}">
      <!--<md-icon>view_list</md-icon>-->
      View All Tasks
    </router-link>
    <br/><br/>
    <md-whiteframe md-tag="section" md-elevation="1">
      <md-toolbar>
        <span style="flex: 1" md-hide-small></span>
        <md-button class="md-icon-button" @click="firstPage">
          <md-icon>first_page</md-icon>
        </md-button>
        <md-button class="md-icon-button" @click="previousPage">
          <md-icon>chevron_left</md-icon>
        </md-button>
        <span>Page: {{paging.page}}</span>
        <md-button class="md-icon-button" @click="nextPage">
          <md-icon>chevron_right</md-icon>
        </md-button>
        <md-button class="md-icon-button" @click="lastPage">
          <md-icon>last_page</md-icon>
        </md-button>
        <span style="flex: 1"></span>
        <md-layout md-flex="20" md-flex-small="25" md-flex-xsmall="80">
          <md-input-container md-inline>
            <md-tooltip md-direction="top">Search using title, description, user etc</md-tooltip>
            <label>Search</label>
            <md-input v-model="paging.filter">
            </md-input>
          </md-input-container>
        </md-layout>
        <md-menu md-direction="bottom left" md-size="3">
          <md-button md-menu-trigger class="md-icon-button">
            <md-tooltip md-direction="top">Apply filter</md-tooltip>
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
      <md-layout md-gutter>
        <div class="flex-vertical min-height full-width" v-show="!Tasks.Trash.content.length">
          <p v-show="Tasks.Trash.loading" class="no-content">
            <md-spinner md-indeterminate class="md-accent" v-show="Tasks.Trash.loading"></md-spinner><br/>
            <span>Fetching data!</span>
          </p>
          <p v-show="!Tasks.Trash.loading" class="no-content">
            <md-icon class="md-accent md-size-2x" md-size-2x>cloud_queue</md-icon><br/>
            <span>{{Tasks.Trash.message || "Awww... Nothing here!"}} <span v-show="Tasks.Trash.error">An error occured while trying to fetch data.</span></span>
          </p>
        </div>
        <transition-group name="list-out" tag="ul" class="no-padding">
          <li class="list-out-item" v-for="Task in Tasks.Trash.content" v-bind:key="Task.MainTaskID">
            <task-card-trash @remove-task-item="removeTaskItem" :Task="Task"></task-card-trash>
          </li>
        </transition-group>
        <md-snackbar md-position="bottom center" ref="snackbar" md-duration="60000">
          <span>Unable to fetch data!<br/> If the problem persists please contact support.</span>
          <md-button class="md-accent" @click="retry">Retry</md-button>
        </md-snackbar>
      </md-layout>
    </md-whiteframe>
  </div>
</template>
<script>
  import _ from 'lodash';
  import taskCardTrash from 'components/_custom/task-card-trash.vue';
  import {
    listTrash
  } from 'services/tasks';

  import {
    getCategories
  } from 'services/catalogs';

  export default {
    name: 'task-list',
    components: {
      'task-card-trash': taskCardTrash
    },
    data: function () {
      return {
        Tasks: {
          Trash: {
            name: 'Assigned',
            icon: 'assignment_return',
            content: [],
            loading: true
          }
        },
        Catalog: {
          Categories: []
        },
        paging: {
          page: 1,
          orderby: '',
          filter: ''
        }
      }
    },
    watch: {
      'Tasks.Trash.content': function () {
        this.$set(this.Tasks.Trash, 'loading', false);
      },
      'paging.filter': function () {
        this.search();
      }
    },
    computed: {
      settings: function () {
        return this.$store.getters.getSettings;
      }
    },
    methods: {
      retry: function () {
        const _self = this;
        _self.$refs.snackbar.close();
        window.setTimeout(function () {
          _self.loadTrash();
        }, 500);
      },
      removeTaskItem: function (obj) {
        const _self = this;
        var id = obj.id;
        var ts = _self.Tasks.Trash.content;

        for (var i = 0; i < ts.length; i++) {
          if (ts[i].MainTaskID == id) {
            _self.Tasks.Trash.content.splice(i, 1);
            break;
          }
        }
      },
      loadTrash: function () {
        const _self = this;
        _self.$set(_self.Tasks.Trash, 'loading', true);
        listTrash(_self.paging).then(res => {
          _self.$set(_self.Tasks.Trash, 'content', res.data);
        }).catch(err => {
          _self.$set(_self.Tasks.Trash, 'loading', false);
          _self.$refs.snackbar.open();
        });
      },
      search: function () {
        const _self = this;
        _self.loadTrash();
      },
      nextPage: function () {
        this.$set(this.paging, 'page', this.paging.page + 1);
        this.search();
      },
      previousPage: function () {
        this.$set(this.paging, 'page', this.paging.page - 1);
        this.search();
      },
      firstPage: function () {
        this.$set(this.paging, 'page', 1);
        this.search();
      },
      lastPage: function () {
        this.$set(this.paging, 'page', 0);
        this.search();
      }
    },
    mounted: function () {
      const _self = this;

      window.setTimeout(function () {
        _self.loadTrash();
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

</style>
