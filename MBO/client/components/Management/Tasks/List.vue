<template>
  <div>
    <md-layout md-flex="100"><span class="md-display-1">Assigned To Me</span></md-layout>
    <md-layout md-gutter>
      <md-spinner md-indeterminate class="md-accent" v-show="isLoading"></md-spinner>
      <task-card v-for="Task in Tasks.Assigned" :Task="Task"></task-card>
    </md-layout>
    <br/>
    <md-layout md-flex="100"><span class="md-display-1">Assigned By Me</span></md-layout>
    <md-layout md-gutter>
      <md-spinner md-indeterminate class="md-accent" v-show="isLoading"></md-spinner>
      <task-card v-for="Task in Tasks.Created" :Task="Task"></task-card>
    </md-layout>
  </div>
</template>
<script>
  import taskCard from 'components/_custom/task-card.vue';
  import {
    listAssigned,
    listCreated
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
      'task-card': taskCard
    },
    data: function () {
      return {
        Tasks: {
          Assigned: [],
          Created: []
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

      getUsersList().then(res => {
        _self.$set(_self.Catalog, 'Users', res.data);
      }).catch(err => {});

      getCategories().then(res => {
        _self.$set(_self.Catalog, 'Categories', res.data);
      }).catch(err => {});

    }
  }

</script>
