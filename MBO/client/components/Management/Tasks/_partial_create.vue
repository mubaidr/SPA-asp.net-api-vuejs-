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
          <label>Title</label>
          <md-input v-model="Title" type="text" name="Title" v-validate data-vv-name="Title" data-vv-rules="required|min:5"></md-input>
          <span class="md-error">{{errors.first('Title')}}</span>
        </md-input-container>
        <md-input-container :class="{'md-input-invalid': errors.has('Description')}">
          <label>Description</label>
          <md-input v-model="Description" type="text" name="Description" v-validate data-vv-name="Description" data-vv-rules="required|min:5"></md-input>
          <span class="md-error">{{errors.first('Description')}}</span>
        </md-input-container>
        <md-input-container>
          <label for="Categories">Category</label>
          <md-select name="Categories" v-model="Categories">
            <md-option v-for="category in Categories" value="">{{category}}</md-option>
          </md-select>
        </md-input-container>
        <md-input-container>
          <label for="Users">Users</label>
          <md-select name="Users" multiple v-model="Users">
            <md-option v-for="user in Users" value="">{{user}}</md-option>
          </md-select>
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

  export default {
    name: 'task-create',
    data: function () {
      return {
        Categories: ['Test', 'Some', 'Category'],
        Title: 'Some title',
        Description: 'Some description',
        Users: ['Test', 'Some', 'User']
      }
    },
    computed: {
      isLoading: function () {
          return this.$store.getters.isLoading;
        }
        //return data in maintasks format for submit,
    },
    methods: {
      loadCategories: {

      },
      loadUsers: {
        //Get Users from API
      },
      formValidate: function (event) {
        event.preventDefault();
        var _self = this;

        _self.$validator.validateAll().then(success => {
          if (!success) return;

          _self.$store.commit('setState', {
            loading: true,
            alert: false
          });

          create(this.$data).then(function (res) {

            _self.$router.push({
              path: '/tasks'
            });

          }).catch(function (err) {
            console.log(err.response);
            _self.$store.commit('isNotLoading');
            _self.$store.commit('setState', {
              err: err
            });

          });

        });
      }
    }

  }

</script>
