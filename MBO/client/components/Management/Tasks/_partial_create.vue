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
          <md-input v-model="Task.Title" maxlength="50" type="text" name="Title" v-validate data-vv-name="Title" data-vv-rules="required|min:5"></md-input>
          <span class="md-error">{{errors.first('Title')}}</span>
        </md-input-container>
        <!--<md-input-container>
          <label>Details <span>(optional)</span></label>
          <md-textarea v-model="Task.Description" maxlength="200" name="Description"></md-textarea>
        </md-input-container>-->
        <!--TODO Set default category in settings-->
        <md-input-container>
          <label for="Categories">Category</label>
          <md-select name="Categories" v-model="Task.Category_Id">
            <md-option v-for="category in Catalog.Categories" :value="category.Category_Id" :title="category.Description">{{category.Title}}</md-option>
          </md-select>
        </md-input-container>
        <md-input-container>
          <label for="Users">Assign To</label>
          <md-select name="Users" multiple v-model="Users">
            <md-option v-for="user in Catalog.Users" :value="user.Id" :title="user.UserName">{{user.UserName}}</md-option>
          </md-select>
        </md-input-container>
        <div>
          <label for="DateDue" class="custom-label">Target Date</label>
          <br/>
          <date-picker :date="datepicker_startTime" :option="datepicker_option" :limit="datepicker_limit" name="DateDue" v-model="Task.DateDue"
            orientation="landscape" autoOk="true"></date-picker>
        </div>
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
  } from 'services/tasks';
  import {
    getUsersList
  } from 'services/account'
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
          Category_Id: 1,
          DateDue: '01-01-2018 12:00'
        },
        Users: [],
        Catalog: {
          Categories: [],
          Users: []
        },
        datepicker_startTime: {
          time: '01-01-2018 12:00'
        },
        datepicker_option: {
          type: 'min',
          format: 'DD-MM-YYYY HH:mm',
          inputStyle: {
            'width': '100%',
            'height': '32px',
            'font-size': '15px',
            'border': 'none',
            'border-bottom': '1px solid #e1e1e1',
            'box-shadow': 'none',
            'color': 'rgba(0,0,0,.87)',
            'font-weight': 'bold',
            'cursor': 'pointer'
          },
          week: ['Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa', 'Su'],
          month: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October',
            'November', 'December'
          ],
          autoOk: true
        },
        datepicker_limit: [{
          type: 'weekday',
          available: [1, 2, 3, 4, 5]
        }]
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

          create({
            mainTask: this.Task,
            users: this.Users
          }).then(function (res) {

            _self.$router.push({
              path: '/tasks'
            });

          }).catch(function (err) {

            _self.$store.commit('setState', {
              loading: false,
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

        getUsersList().then(res => {
          _self.$set(_self.Catalog, 'Users', res.data);
        }).catch(err => {});

      }).catch(err => {});

    }
  };

</script>
