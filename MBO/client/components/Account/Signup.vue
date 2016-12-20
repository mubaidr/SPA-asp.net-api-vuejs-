<template>
  <div>
    <div class="row">
      <div class="col-md-offset-4 col-md-4">
        <h1>Signup</h1>
        <p>Please provide following details to create a new account.</p>
        <div v-show="progress.statusMessage" class="alert alert-danger">
          {{progress.statusMessage}}
        </div>
        <div v-show="validationSummary.messages.length" class="alert alert-danger">
          <p>Please fix following errors and then submit again:</p>
          <ul class="validation-list">
            <transition-group name="slide-fade" mode="out-in">
              <li v-for="message in validationSummary.messages" :key="message">
                {{message}}
              </li>
            </transition-group>
          </ul>
        </div>
        <md-input-container>
          <label>Username</label>
          <md-input v-model="username"></md-input>
        </md-input-container>
        <div class="form-group" v-bind:class="{ 'has-error': $v.credentials.email.$error }">
          <label for="email" class="control-label">Email</label>
          <input v-model="credentials.email" type="email" name="email" class="form-control" placeholder="email" v-model.trim="credentials.email"
            @input="$v.credentials.email.$touch()" required>
        </div>
        <div class="form-group" v-bind:class="{ 'has-error': $v.credentials.password.$error }">
          <label for="password" class="control-label">Password</label>
          <input v-model="credentials.password" type="password" name="password" class="form-control" placeholder="Password" v-model.trim="credentials.password"
            @input="$v.credentials.password.$touch()">
        </div>
        <div class="form-group" v-bind:class="{ 'has-error': $v.credentials.confirmPassword.$error }">
          <label for="confirmPassword" class="control-label">Confirm Password</label>
          <input v-model="credentials.confirmPassword" type="password" name="confirmPassword" class="form-control" placeholder="Confirm Password"
            v-model.trim="credentials.confirmPassword" @input="$v.credentials.confirmPassword.$touch()">
        </div>
        <div class="form-group">
          <button v-on:click="register" class="btn btn-block btn-primary">Create Account</button>
        </div>
        <div class="form-group">
          Already have an account?
          <router-link to="/login">Signin here!</router-link>
          <br/> Forgot password?
          <router-link to="/recover">Recover now!</router-link>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
  import {
    required,
    sameAs,
    minLength,
    alphaNum
  } from 'vuelidate/lib/validators'
  import email from 'utilities/validators/email'
  import axios from 'axios'
    import 'utilities/string'
  import {
    Summary
  } from 'utilities/validationSummary'
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
    validations: {
      credentials: {
        email: {
          required,
          email,
          minLength: minLength(5)
        },
        password: {
          required,
          alphaNum,
          minLength: minLength(5)
        },
        confirmPassword: {
          required,
          // alphaNum,
          // minLength: minLength(5),
          sameAs: sameAs('password')
        }
      }
    },
    computed: {
      validationSummary: Summary
    },
    methods: {
      register: function (event) {
        event.preventDefault();
        var _self = this;
        _self.$v.$touch();

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

        }
      }
    },
    ready: function () {
      console.log('ready');
    }
  }

</script>
