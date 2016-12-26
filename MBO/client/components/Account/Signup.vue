<template>
  <md-layout md-gutter>
	<md-layout md-hide-small>
	</md-layout>
	<md-layout>
	  <md-card class="full-width">
		<md-card-media>
		  <md-ink-ripple></md-ink-ripple>
		  <img src="../../assets/backgrounds/20.jpg" alt="sun" class="card-media-animation">
		</md-card-media>
		<md-card-header>
		  <md-card-header-text>
			<div class="md-title">Create an account</div>
			<div class="md-subhead">Please provide required information.</div>
		  </md-card-header-text>
		  <md-spinner md-indeterminate class="md-accent" v-show="status.loading"></md-spinner>
		</md-card-header>
		<md-card-content>    
		  <md-input-container :class="{'md-input-invalid': errors.has('username')}">
			<label>Username</label>
			<md-input v-model="credentials.username" type="username" name="username" v-validate data-vv-name="username" data-vv-rules="required|alpha|min:5"
			  :disabled="status.loading"></md-input>
			<span class="md-error">{{errors.first('username')}}</div>
		  </md-input-container>
		  <md-input-container md-has-password :class="{'md-input-invalid': errors.has('password')}">
			<label>Password</label>
			<md-input v-model="credentials.password" type="password" name="password" v-validate data-vv-name="password" data-vv-rules="required|alpha|min:5" :disabled="status.loading"></md-input>
			<span class="md-error">{{errors.first('password')}}</div>
		  </md-input-container>
		  <md-input-container md-has-password :class="{'md-input-invalid': errors.has('confirmPassword')}">
			<label>Confirm Password</label>
			<md-input v-model="credentials.confirmPassword" type="password" name="confirmPassword" v-validate data-vv-name="confirmPassword" data-vv-rules="required|confirmed:password" :disabled="status.loading"></md-input>
			<span class="md-error">{{errors.first('confirmPassword')}}</div>
		  </md-input-container>
		</md-card-content>
		<md-card-actions>
		  <router-link tag="md-button" to="/signin" class="md-accent">Already have an account?</router-link>
		  <md-button class="md-raised md-accent" @click="validateBeforeSubmit" :disabled="status.loading">Register</md-button>
		</md-card-actions>
		<md-card-content>
		  <div v-show="!status.valid" class="red">
			  {{status.message}}
		  </div>
		  <!--<pre>{{errors.collect()}}</pre>-->
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
          username: 'tester',
          password: 'tester',
          confirmPassword: 'tester'
        },
        status: {
          loading: false,
          valid: true,
          message: ''
        }
      }
    },
    methods: {
      validateBeforeSubmit: function (event) {
      var _self = this;
      event.preventDefault();
      _self.$validator.validateAll().then(success => {
          if (!success) {
          return;
          }
          _self.formSubmit();
        });
      },
      formSubmit: function () {
        var _self = this;
        _self.status.loading = true;
        signup({
          Email: this.credentials.username,
          Password: this.credentials.password,
          ConfirmPassword: this.credentials.confirmPassword
        }).then(function (res) {
          _self.status.loading = false;
          console.log('res', res);
        }).catch(function (err) {
          _self.status.loading = false;
          console.log('res', err);
        });
      }
    },
    ready: function () {
      console.log('ready');
    }
  }
</script>
