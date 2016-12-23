<template>
  <md-layout md-gutter>
    <md-layout md-hide-small>
    </md-layout>
    <md-layout>
      <md-card class="full-width">
        <md-card-media>
          <md-ink-ripple></md-ink-ripple>
          <img src="../../assets/backgrounds/33.jpg" alt="sun" class="card-media-animation">
        </md-card-media>
        <md-card-header>
          <md-card-header-text>
            <div class="md-title">Create an account</div>
            <div class="md-subhead">Please provide required information.</div>
          </md-card-header-text>
          <md-spinner md-indeterminate class="md-accent" v-show="status.loading"></md-spinner>
        </md-card-header>
        <md-card-content>
          <md-input-container :class="{'md-input-invalid': errors.has('email')}">
            <label>Username</label>
            <md-input v-model="credentials.email" type="email" v-model.trim="credentials.email" v-validate="credentials.email" data-vv-rules="required|alpha|min:5"
              :disabled="status.loading"></md-input>
            <span class="md-error">{{errors.first('email')}}</div>
          </md-input-container>
          <md-input-container md-has-password :class="{'md-input-invalid': errors.has('password')}">
            <label>Password</label>
            <md-input v-model="credentials.password" type="password" v-model.trim="credentials.password" v-validate="credentials.password" data-vv-rules="required|alpha|min:5" :disabled="status.loading"></md-input>
            <span class="md-error">{{errors.first('password')}}</div>
          </md-input-container>
          <md-input-container md-has-password :class="{'md-input-invalid': errors.has('confirmPassword')}">
            <label>Confirm Password</label>
            <md-input v-model="credentials.confirmPassword" type="password" v-model.trim="credentials.confirmPassword" v-validate.initial="credentials.confirmPassword" data-vv-rules="required|alpha|min:5" :disabled="status.loading"></md-input>
            <span class="md-error">{{errors.first('confirmPassword')}}</div>
          </md-input-container>
        </md-card-content>
        <md-card-actions>
          <router-link tag="md-button" to="/signin" class="md-accent">Already have an account?</router-link>
          <md-button class="md-raised md-accent" @click="signup" :disabled="status.loading">Register</md-button>
        </md-card-actions>
        <md-card-content>
          <div v-show="status.message" class="red">
            {{status.message}}
          </div>
          <pre>{{errors.collect()}}</pre>
        </md-card-content>
      </md-card>
    </md-layout>
    <md-layout md-hide-small></md-layout>
  </md-layout>
</template>
<script>
  import axios from 'axios'
  import {
    signup
  } from 'services/account'

  export default {
    data: function () {
      return {
        credentials: {
          email: '',
          password: '',
          confirmPassword: ''
        },        
        status: {
          loading: false,
          message: ''
        }
      }
    },
    methods: {
      signup: function (event) {
        event.preventDefault();
        var _self = this;
        if (_self.errors.length) {
          return false;
        } else {
            _self.status.loading = true;
          signup({
            Email: this.credentials.email,
            Password: this.credentials.password,
            ConfirmPassword: this.credentials.confirmPassword
          }).then(function (res) {
            _self.status.loading = false;
            console.log(res);
          }).catch(function (err) {
            _self.status.loading = false;
            console.log(err);
          });
      }
      }
    },
    ready: function () {
      console.log('ready');
    }
  }

</script>
