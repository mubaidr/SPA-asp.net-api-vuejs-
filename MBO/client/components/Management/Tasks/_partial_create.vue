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
          <md-input v-model="Task.Title" type="text" name="Title" v-validate data-vv-name="Title" data-vv-rules="required|min:5"></md-input>
          <span class="md-error">{{errors.first('Title')}}</span>
        </md-input-container>
        <md-input-container>
          <label>Description</label>
          <md-textarea v-model="Task.Description" name="Description"></md-textarea>
        </md-input-container>
        <md-input-container>
          <label for="Categories">Category</label>
          <md-select name="Categories" v-model="Task.Category">
            <md-option v-for="category in Catalog.Categories" :value="category.Category_Id" :title="category.Description">{{category.Title}}</md-option>
          </md-select>
        </md-input-container>
        <md-input-container>
          <label for="Users">Users</label>
          <md-select name="Users" multiple v-model="Task.Users">
            <!--<md-option v-for="user in Catalog.Users" :value="user.User_Id" :title="user.email">{{user.FullName}}</md-option>-->
          </md-select>
        </md-input-container>
        <app-message></app-message>
        <pre>
          {{Task.Category}}
        </pre>
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
  } from 'services/tasks';
  import {
    getCategories
  } from 'services/catalogs';

  export default {
    name: 'task-create',
    data: function () {
      return {
        Task: {
          Title: '',
          Description: '',
          Category: 1,
          Users: []
        },
        Catalog: {
          Categories: [],
          Users: []
        }
      }
    },
    computed: {
      isLoading: function () {
        return this.$store.getters.isLoading;
      }
    },
    methods: {
      formValidate(event) {
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

            _self.$store.commit('isNotLoading');
            _self.$store.commit('setState', {
              err: err
            });

          });

        });
      }
    },
    mounted: function () {
      const _self = this;
      getCategories().then(res => {
        _self.$set(_self.Catalog, 'Categories', res.data);
      }).catch(err => {});
    }
  };

</script>
