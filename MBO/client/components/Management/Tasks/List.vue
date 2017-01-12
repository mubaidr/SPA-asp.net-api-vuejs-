<template>
  <div>
    <md-tabs md-right>
      <md-tab md-label="Assigned to me" md-icon="assignment_returned">
        <md-toolbar class="md-transparent">
          <span style="flex: 1"></span>
          <md-button class="md-icon-button">
            <md-icon>search</md-icon>
          </md-button>
          <md-menu md-direction="bottom left" md-size="3">
            <md-button md-menu-trigger class="md-icon-button">
              <md-icon>filter_list</md-icon>
            </md-button>
            <md-menu-content>
              <md-menu-item disabled>Date</md-menu-item>
              <md-menu-item>Ascending</md-menu-item>
              <md-menu-item>Descending</md-menu-item>
              <md-menu-item disabled>Priority</md-menu-item>
              <md-menu-item>Low</md-menu-item>
              <md-menu-item>High</md-menu-item>
            </md-menu-content>
          </md-menu>
        </md-toolbar>
        <md-layout md-gutter>
          <md-spinner md-indeterminate class="md-accent" v-show="isLoading"></md-spinner>
          <task-card v-for="Task in Tasks.Assigned" :Task="Task"></task-card>
          <div class="flex-vertical full-width" v-show="!Tasks.Assigned.length">
            <p>
              <md-icon class="md-accent md-size-4x">info_outline</md-icon>You have been not assigned any task.
            </p>
          </div>
        </md-layout>
      </md-tab>
      <md-tab md-label="Created by Me" md-icon="assignment_return">
        <md-layout md-gutter>
          <md-spinner md-indeterminate class="md-accent" v-show="isLoading"></md-spinner>
          <task-card v-for="Task in Tasks.Created" :Task="Task"></task-card>
          <div class="flex-vertical full-width" v-show="!Tasks.Created.length">
            <p>
              <md-icon class="md-accent md-size-4x">info_outline</md-icon>Nothing here.
            </p>
          </div>
        </md-layout>
      </md-tab>
      <md-tab md-label="Completed" md-icon="assignment_turned_in">
        <md-layout md-gutter>
          <md-spinner md-indeterminate class="md-accent" v-show="isLoading"></md-spinner>
          <task-card v-for="Task in Tasks.Completed" :Task="Task"></task-card>
          <div class="flex-vertical full-width" v-show="!Tasks.Completed.length">
            <p>
              <md-icon class="md-accent md-size-4x">info_outline</md-icon>You have not completed any task yet!
            </p>
          </div>
        </md-layout>
      </md-tab>
      <md-tab md-label="Create New" md-icon="assignment">
        <md-layout md-gutter>
          <md-layout md-hide-small></md-layout>
          <md-layout>
            <task-create></task-create>
          </md-layout>
          <md-layout md-hide-small></md-layout>
        </md-layout>
      </md-tab>
    </md-tabs>
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
          Assigned: [],
          Created: [],
          Completed: []
        },
        Catalog: {
          Categories: [],
          Users: []
        }
      }
    },
    computed: {
      isLoading: function () {
        return this.$store.state.page.loading;
      }
    },
    mounted: function () {
      const _self = this;

      listAssigned().then(res => {
        console.log(res.data);
        _self.$set(_self.Tasks, 'Assigned', res.data);
      }).catch(err => {});

      listCreated().then(res => {
        _self.$set(_self.Tasks, 'Created', res.data);
      }).catch(err => {});

      listCompleted().then(res => {
        _self.$set(_self.Tasks, 'Completed', res.data);
      }).catch(err => {});

      getUsersList().then(res => {
        _self.$set(_self.Catalog, 'Users', res.data);
      }).catch(err => {});

      getCategories().then(res => {
        _self.$set(_self.Catalog, 'Categories', res.data);
      }).catch(err => {});

    }
  }

</script>
