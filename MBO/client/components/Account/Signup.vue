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
            </md-card-header-text>
            <md-spinner md-indeterminate :md-stroke="4" class="md-accent" v-show="request.loading"></md-spinner>
          </md-card-header>
          <md-card-content>
            <md-input-container :class="{'md-input-invalid': errors.has('Email')}">
              <label>Email</label>
              <md-input v-model="credentials.Email" type="email" name="Email" v-validate data-vv-name="Email" data-vv-rules="required|email|min:6"></md-input>
              <span class="md-error">{{errors.first('Email')}}</span>
            </md-input-container>
            <md-input-container md-has-password :class="{'md-input-invalid': errors.has('password')}">
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
            <div class="form-error" v-show="!request.valid">
              <p>{{page.message}}</p>
              <ul>
                <li v-for="detail in page.details">
                  {{detail}}
                </li>
              </ul>
            </div>
          </md-card-content>
          <md-card-actions>
            <!--<pre>this: {{page}}</pre>
            <br/>
            <pre>this: {{request}}</pre>-->
            <router-link tag="md-button" to="/signin" class="md-accent">Already have an account?</router-link>
            <md-button id="btnSubmit" class="md-raised md-accent" @click="formValidate" :disabled="request.loading">Register</md-button>
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
    signup,
    signin
  } from 'services/account'

  export default {
    data: function () {
      return {
        credentials: {
          Email: 'tester5@test.com',
          Password: 'tester1234',
          ConfirmPassword: 'tester1234'
        }
      }
    },
    computed: {
      page: function () {
        return this.$store.getters.page;
      },
      request: function () {
        return this.$store.getters.request;
      }
    },
    methods: {
      formValidate: function (event) {
        var _self = this;
        event.preventDefault();
        _self.$validator.validateAll().then(success => {
          if (!success) return;

          _self.$store.commit('setState', {
            loading: true,
            valid: true
          });

          signup(this.credentials).then(function (res) {
            _self.$store.commit('setState', {
              loading: false,
              valid: true,
              err: {}
            });

            //TODO $router go to login with message
            _self.$router.go('login');
          }).catch(function (err) {
            _self.$store.commit('setState', {
              loading: false,
              valid: false,
              err: err
            });
          });
        });

      }
    }
  }

</script>
