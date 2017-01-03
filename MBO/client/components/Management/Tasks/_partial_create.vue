<template>
  <div class="flex-vertical full-width">
    <md-card class="full-width">
      <md-card-header class="indigo">
        <md-card-header-text>
          <div class="md-title">Get started</div>
          <span>Create</span>
        </md-card-header-text>
        <md-spinner md-indeterminate class="md-accent" v-show="isLoading"></md-spinner>
      </md-card-header>
      <md-card-content>
        <md-input-container :class="{'md-input-invalid': errors.has('Title')}">
        </md-input-container>
        <md-input-container md-has-password :class="{'md-input-invalid': errors.has('Description')}">
        </md-input-container>
        <app-message></app-message>
      </md-card-content>
      <md-card-actions>
        <router-link tag="md-button" to="/tasks" class="md-accent">View Tasks</router-link>
        <md-button id="btnSubmit" class="md-raised md-accent" @click="formValidate" :disabled="isLoading">Create New Task</md-button>
      </md-card-actions>
    </md-card>
  </div>
</template>
<script>
  import {
    create
  } from 'services/tasks'
  import appMessage from 'components/_custom/app-message.vue'

  export default {
    name: 'task-create',
    components: {
      'app-message': appMessage
    },
    data: function () {
      return {
        Title: 'Some title',
        Description: 'Some description',
        Users: []
      }
    },
    computed: {
      isLoading: function () {
        return this.$store.getters.isLoading;
      }
    },
    methods: {
      formValidate: function (event) {
        event.preventDefault();
        var _self = this;

        _self.$validator.validateAll().then(success => {
          if (!success) return;
          _self.$store.commit('isLoading');

          create(this.$data).then(function (res) {

            _self.$router.push({
              path: '/tasks'
            });

          }).catch(function (err) {

            _self.$store.commit('isNotLoading');

          });

        });
      }
    }

  }

</script>
