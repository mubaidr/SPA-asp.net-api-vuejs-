<template>
  <md-layout md-gutter>
    <md-layout md-hide-xsmall></md-layout>
    <md-layout md-hide-medium></md-layout>
    <md-layout>
      <div class="flex-vertical full-width">
        <md-card class="full-width">
          <md-card-header class="indigo">
            <md-card-header-text>
              <div class="md-title">Create a new account</div>
              <span>Sign Up</span>
            </md-card-header-text>
            <md-spinner md-indeterminate :md-stroke="4" class="md-accent" v-show="isLoading"></md-spinner>
          </md-card-header>
          <md-card-content>
            <md-input-container :class="{'md-input-invalid': errors.has('Email')}">
              <label>Email</label>
              <md-input v-model="credentials.Email" type="email" name="Email" v-validate data-vv-name="Email" data-vv-rules="required|email|min:6"></md-input>
              <span class="md-error">{{errors.first('Email')}}</span>
            </md-input-container>
            <md-input-container md-has-password :class="{'md-input-invalid': errors.has('Password')}">
              <label>Password</label>
              <md-input v-model="credentials.Password" type="password" name="Password" v-validate data-vv-name="Password" data-vv-rules="required|min:6"></md-input>
              <span class="md-error">{{errors.first('Password')}}</span>
            </md-input-container>
            <md-input-container md-has-password :class="{'md-input-invalid': errors.has('ConfirmPassword')}">
              <label>Confirm Password</label>
              <md-input v-model="credentials.ConfirmPassword" type="password" name="ConfirmPassword" v-validate data-vv-name="ConfirmPassword"
                data-vv-rules="required|confirmed:Password"></md-input>
              <span class="md-error">{{errors.first('ConfirmPassword')}}</span>
            </md-input-container>
            <app-message></app-message>
          </md-card-content>
          <md-card-actions>
            <router-link tag="md-button" to="/signin" class="md-accent">Already have an account?</router-link>
            <md-button id="btnSubmit" class="md-raised md-accent" @click="formValidate" :disabled="isLoading">Sign Up</md-button>
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
    signup
  } from 'services/account'

  export default {
    data: function () {
      return {
        credentials: {
          Email: '',
          Password: '',
          ConfirmPassword: ''
        }
      }
    },
    computed: {
      isLoading: function () {
        return this.$store.getters.isLoading;
      },
    },
    methods: {
      formValidate: function (event) {
        event.preventDefault();
        var _self = this;

        _self.$validator.validateAll().then(success => {
          if (!success) return;

          signup(this.credentials).then(function (res) {

            _self.$router.push({
              path: '/signin',
              params: {
                message: 'signup-success'
              }
            });

          }).catch(error => {});

        });

      }
    }
  }

</script>
