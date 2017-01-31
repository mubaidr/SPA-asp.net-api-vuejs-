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
        <div class="flex-vertical min-height full-width" v-show="!Tasks.Trash.prop.content.length">
          <p v-show="Tasks.Trash.prop.loading" class="no-content">
            <md-spinner md-indeterminate class="md-accent" v-show="Tasks.Trash.prop.loading"></md-spinner><br/>
            <span>Fetching data!</span>
          </p>
          <p v-show="!Tasks.Trash.prop.loading" class="no-content">
            <md-icon class="md-accent md-size-2x" md-size-2x>cloud_queue</md-icon><br/>
            <span>{{Tasks.Trash.message || "Awww... Nothing here!"}} <span v-show="Tasks.Trash.error">An error occured while trying to fetch data.</span></span>
          </p>
        </div>
        <task-card-trash v-for="Task in Tasks.Trash.prop.content" :Task="Task"></task-card-trash>
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
      openDialog: function (ref) {
        this.$refs[ref].open();
      },
      closeDialog: function (ref) {
        this.$refs[ref].close();
      },
      loadTrash: function () {
        const _self = this;
        listTrash().then(res => {
          _self.$set(_self.Tasks.Trash, 'prop', {
            content: res.data,
            loading: false,
            error: false
          });
        }).catch(err => {
          _self.$set(_self.Tasks.Trash, 'prop', {
            content: [],
            loading: false,
            error: true
          });
        });
      },
      removeTask: function (id) {
        const _self = this;
        const len = _self.prop.content.length;

        for (var i = 0; i < len; i++) {
          var obj = _self.prop.content[i];

          if (id !== obj.MainTaskID) {
            _self.prop.content.splice(i, 1);
          }
        }
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
  
  .no-padding {
    padding: 0;
  }

</style>
