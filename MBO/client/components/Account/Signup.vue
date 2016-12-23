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
          <md-spinner md-indeterminate class="md-accent" v-show="progress.loading"></md-spinner>
        </md-card-header>
        <md-card-content>
          <md-input-container :class="{'md-input-invalid': errors.has('email')}">
            <label>Username</label>
            <md-input v-model="credentials.email" type="email" v-model.trim="credentials.email" v-validate.initial="credentials.email"
              data-vv-rules="required|alpha|min:5"></md-input>
            <span class="md-error">{{errors.first('email')}}</div>
          </md-input-container>
          <md-input-container md-has-password :class="{'md-input-invalid': errors.has('password')}">
            <label>Password</label>
            <md-input v-model="credentials.password" type="password" v-model.trim="credentials.password" v-validate.initial="credentials.password" data-vv-rules="required|alpha|min:5"></md-input>
            <span class="md-error">{{errors.first('password')}}</div>
          </md-input-container>
          <md-input-container md-has-password :class="{'md-input-invalid': errors.has('confirmPassword')}">
            <label>Confirm Password</label>
            <md-input v-model="credentials.confirmPassword" type="password" v-model.trim="credentials.confirmPassword" v-validate.initial="credentials.confirmPassword" data-vv-rules="required|alpha|min:5"></md-input>
            <span class="md-error">{{errors.first('confirmPassword')}}</div>
          </md-input-container>
        </md-card-content>
        <md-card-actions>
          <router-link tag="md-button" to="/signin" class="md-accent">Already have an account?</router-link>
          <md-button class="md-raised md-accent" @click="signup">Register</md-button>
        </md-card-actions>
        <md-card-content>
          <div v-show="progress.statusMessage" class="alert alert-danger">
            {{progress.statusMessage}}
          </div>
          <pre>{{errors.has('email')}}</pre>
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
        progress: {
          loading: false,
          statusMessage: ''
        }
      }
    },
    computed: {

    },
    methods: {
      signup: function (event) {
        event.preventDefault();
        var _self = this;
        /*
        //_self.$v.$touch();

        if (_self.$v.$invalid) {
          return false;
        } else {

          signup({
            Email: this.credentials.email,
            Password: this.credentials.password,
            ConfirmPassword: this.credentials.confirmPassword
          }).then(function (res) {
            console.log(res);
          }).catch(function (err) {
            console.log(err);
          });

      }*/
      }
    },
    ready: function () {
      console.log('ready');
    }
  }

</script>
