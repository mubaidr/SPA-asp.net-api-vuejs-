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
		  <md-input-container :class="{'md-input-invalid': errors.has('Email')}">
			<label>Email</label>
			<md-input v-model="credentials.Email" type="email" name="Email" v-validate data-vv-name="Email" data-vv-rules="required|email|min:5"
			  :disabled="status.loading"></md-input>
			<span class="md-error">{{errors.first('Email')}}</div>
		  </md-input-container>
		  <md-input-container md-has-password :class="{'md-input-invalid': errors.has('password')}">
			<label>Password</label>
			<md-input v-model="credentials.Password" type="password" name="Password" v-validate data-vv-name="Password" data-vv-rules="required|min:5" :disabled="status.loading"></md-input>
			<span class="md-error">{{errors.first('Password')}}</div>
		  </md-input-container>
		  <md-input-container md-has-password :class="{'md-input-invalid': errors.has('ConfirmPassword')}">
			<label>Confirm Password</label>
			<md-input v-model="credentials.ConfirmPassword" type="password" name="ConfirmPassword" v-validate data-vv-name="ConfirmPassword" data-vv-rules="required|confirmed:Password" :disabled="status.loading"></md-input>
			<span class="md-error">{{errors.first('ConfirmPassword')}}</div>
		  </md-input-container>
		</md-card-content>
		<md-card-actions>
		  <router-link tag="md-button" to="/signin" class="md-accent">Already have an account?</router-link>
		  <md-button id="btnSubmit" class="md-raised md-accent" @click="validateBeforeSubmit" :disabled="status.loading">Register</md-button>
		</md-card-actions>
		<md-card-content>    
      <md-dialog  md-open-from="#btnSubmit" md-close-to="#btnSubmit" ref="warning-alert" @open="dialogOpened" @close="dialogClosed">
        <md-dialog-title><md-icon>alert</md-icon> Error</md-dialog-title>
        <md-dialog-content>
          <p>{{status.message}}</p>          
        </md-dialog-content>
        <md-dialog-actions>
          <md-button class="md-primary" @click="dialogClose">Retry</md-button>
        </md-dialog-actions>
      </md-dialog>
		</md-card-content>
	  </md-card>
	</md-layout>
	<md-layout md-hide-small></md-layout>
  </md-layout>
</template>

<script>
  import {
    signup, signin
  } from 'services/account'

  export default {
    data: function () {
      return {
        credentials: {
          Email: 'tester@test.com',
          Password: 'tester1234',
          ConfirmPassword: 'tester1234'
        },
        status: {
          loading: false,
          valid: true,
          message: ' '
        }
      }
    },
    methods: {
      validateBeforeSubmit: function (event) {
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

        signup(this.credentials).then(function (res) {
          _self.status.loading = false;
          _self.status.valid = true;

          //TODO clear from
          //TODO show login progress
          //TODO redirect to dashboard
          //TODO check already exisitng user

          console.log('res', res);
        }).catch(function (err) {
          _self.status.loading = false;
          _self.status.valid = false;

          _self.status.message = err.response.data.Message;
          _self.dialogOpen();
        });

      },
      dialogOpen: function(){
        this.$refs['warning-alert'].open();
      },
      dialogClose: function(){
        this.$refs['warning-alert'].close();
      },
      dialogOpened: function(){
      },
      dialogClosed: function(){
        _self.status.valid = true;
      }
    },
    ready: function () {
      console.log('ready');
    }
  }

</script>
