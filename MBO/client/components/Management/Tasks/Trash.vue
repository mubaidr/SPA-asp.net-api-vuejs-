<template>
  <div>
    <md-whiteframe md-tag="section" md-elevation="0">
      <span class="md-display-1">Archived Tasks</span>
      <md-toolbar class="md-transparent no-padding">
        <router-link class="md-accent" :to="{path: '/tasks'}">
          <md-icon>view_list</md-icon>
          View All Tasks</router-link>
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
      </md-layout>
    </md-whiteframe>
  </div>
</template>
<script>
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
        }
      }
    },
    watch: {
      'Tasks.Trash.content': function () {
        this.$set(this.Tasks.Trash, 'loading', false);
      }
    },
    computed: {
      settings: function () {
        return this.$store.getters.getSettings;
      }
    },
    methods: {
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
        listTrash().then(res => {
          _self.$set(_self.Tasks.Trash, 'content', res.data);
        }).catch(err => {});
      }
    },
    mounted: function () {
      const _self = this;

      window.setTimeout(function () {
        _self.loadTrash();
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

</style>
