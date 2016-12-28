<template>
  <md-layout md-gutter>
    <md-layout md-hide-xsmall></md-layout>
    <md-layout md-hide-medium></md-layout>
    <md-layout>
      <div class="flex-vertical full-width">
        <md-card class="full-width">
          <md-card-header class="indigo">
            <md-card-header-text>
              <div class="md-title">Sign In</div>
            </md-card-header-text>
            <md-spinner md-indeterminate class="md-accent" v-show="status.loading"></md-spinner>
          </md-card-header>
          <md-card-content>
            <md-input-container :class="{'md-input-invalid': errors.has('Email')}">
              <label>Email</label>
              <md-input v-model="credentials.UserName" type="email" name="Email" v-validate data-vv-name="Email" data-vv-rules="required|email|min:6"></md-input>
              <span class="md-error">{{errors.first('Email')}}</span>
            </md-input-container>
            <md-input-container md-has-password :class="{'md-input-invalid': errors.has('password')}">
              <label>Password</label>
              <md-input v-model="credentials.Password" type="password" name="Password" v-validate data-vv-name="Password" data-vv-rules="required|min:6"></md-input>
              <span class="md-error">{{errors.first('Password')}}</span>
            </md-input-container>
            <div class="form-error" v-show="!status.valid">
              <p>{{status.message}}</p>
              <ul>
                <li v-for="detail in status.details">
                  {{detail}}
                </li>
              </ul>
            </div>
          </md-card-content>
          <md-card-actions>
            <router-link tag="md-button" to="/recover" class="md-accent">Forgot password?</router-link>
            <md-button id="btnSubmit" class="md-raised md-accent" @click="formValidate" :disabled="status.loading">Sing In</md-button>
          </md-card-actions>
        </md-card>
      </div>
    </md-layout>
    <md-layout md-hide-xsmall></md-layout>
    <md-layout md-hide-medium></md-layout>
  </md-layout>
</template>
<script>
  import {
    signin
  } from 'services/account'

  export default {
    data: function () {
      return {
        credentials: {
          UserName: 'tester@test.com',
          Password: 'tester1234_'
        },
        status: {
          loading: false,
          valid: true,
          message: '',
          details: []
        }
      }
    },
    methods: {
      formValidate: function (event) {
        var _self = this;
        event.preventDefault();
        _self.$validator.validateAll().then(success => {
          if (!success) return;
          _self.formSubmit();
        });
      },
      formSubmit: function () {
        var _self = this;
        _self.status.loading = true;

        signin(this.credentials).then(function (res) {
          _self.status.loading = false;
          _self.status.valid = true;



          console.log('res', res);
        }).catch(function (err) {
          _self.status.loading = false;
          _self.status.valid = false;

          _self.status.details.length = 0;
          if (!err.response || !err.response.data) {
            _self.status.message = "Unable to contact server!";
            return;
          } else if (typeof err.response.data.error === 'string') {
            _self.status.message = err.response.data.error;
            _self.status.details.push(err.response.data.error_description);
          } else if (typeof err.response.data.ModelState === 'object') {
            var modelState = err.response.data.ModelState[""];
            _self.status.message = err.response.data.Message;

            for (var i = 0; i < modelState.length; i++) {
              _self.status.details.push(modelState[i]);
            }
          } else {
            _self.status.message = "Something went wrong!";
          }

        });
      }
    }
  }

</script>
