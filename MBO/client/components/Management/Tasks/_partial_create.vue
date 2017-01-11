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
          <md-select name="Categories" v-model="Task.CategoryID">
            <md-option v-for="category in Catalog.Categories" :value="category.CategoryID" :title="category.Description">{{category.Title}}</md-option>
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
            orientation="landscape" autoOk="true" v-validate data-vv-name="DateDue" data-vv-rules="required|date_format:DD-MM-YYYY HH:mm A"></date-picker>
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
  import moment from 'moment';

  export default {
    name: 'task-create',
    data: function () {
      return {
        Task: {
          Title: '',
          Description: '',
          CategoryID: 1,
          DateDue: ''
        },
        Users: [],
        Catalog: {
          Categories: [],
          Users: []
        },
        datepicker_startTime: {
          time: ''
        },
        datepicker_option: {
          type: 'min',
          format: 'DD-MM-YYYY HH:mm A',
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
        return this.$store.state.page.loading;
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

//TOFIX date value
          console.log(_self.Task.DateDue, _self.datepicker_startTime.time);

          return;
          create({
            mainTask: this.Task,
            users: this.Users
          }).then(function (res) {

            // _self.$router.push({
            //   path: '/tasks/list'
            // });

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
      const now = moment().add(1, 'days').format('DD-MM-YYYY HH:mm A');

      _self.$set(_self.Task, 'DateDue', now);
      _self.$set(_self.datepicker_startTime, 'time', now);

      getCategories().then(res => {
        _self.$set(_self.Catalog, 'Categories', res.data);
      }).catch(err => {});

      getUsersList().then(res => {
        _self.$set(_self.Catalog, 'Users', res.data);
      }).catch(err => {});

    }
  };

</script>
