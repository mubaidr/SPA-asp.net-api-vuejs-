<template>
  <div>
    <div class="row">
      <div class="col-md-offset-4 col-md-4">
        <h1>Signup</h1>
        <p>Please provide following details to create a new account.</p>
        <div v-show="formValidation.isInvalid && formValidation.messages.length" class="alert alert-danger">
          <ul class="validation-list">
            <li v-for="message in formValidation.messages">
              {{message}}
            </li>
          </ul>
        </div>
        <div class="form-group" v-bind:class="{ 'has-error': $v.credentials.username.$error }">
          <label for="username" class="control-label">Username</label>
          <input v-model="credentials.username" type="text" name="username" class="form-control" placeholder="Username" v-model.trim="credentials.username"
            @blur="$v.credentials.username.$touch()">
        </div>
        <div class="form-group" v-bind:class="{ 'has-error': $v.credentials.password.$error }">
          <label for="password" class="control-label">Password</label>
          <input v-model="credentials.password" type="password" name="password" class="form-control" placeholder="Password" v-model.trim="credentials.password"
            @blur="$v.credentials.password.$touch()">
        </div>
        <div class="form-group" v-bind:class="{ 'has-error': $v.credentials.confirmPassword.$error }">
          <label for="confirmPassword" class="control-label">Confirm Password</label>
          <input v-model="credentials.confirmPassword" type="password" name="confirmPassword" class="form-control" placeholder="Confirm Password"
            v-model.trim="credentials.confirmPassword" @blur="$v.credentials.confirmPassword.$touch()">
        </div>
        <div class="form-group">
          <button v-on:click="register" class="btn btn-block btn-primary" v-bind:disabled="$v.validationGroup.$invalid">Create Account</button>
        </div>
        <div class="form-group">
          Already have an account?
          <router-link to="/login">Signin here!</router-link>
          <br/> Forgot password?
          <router-link to="/recover">Recover now!</router-link>
        </div>
      </div>
    </div>
    <div v-show="progress.loading" class="backdrop">
      <ring-loader></ring-loader>
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
  import axios from 'axios'
  import RingLoader from 'vue-spinner/src/RingLoader'
  import '../../string'
  import {
    formValidation
  } from '../../formValidation'

  export default {
    components: {
      RingLoader
    },
    data: function () {
      return {
        credentials: {
          username: 'tester',
          password: 'tester',
          confirmPassword: 'tester'
        },
        progress: {
          loading: false,
          successMessage: ''
        }
      }
    },
    validations: {
      credentials: {
        username: {
          required,
          alphaNum,
          minLength: minLength(6)
        },
        password: {
          required,
          alphaNum,
          minLength: minLength(6)
        },
        confirmPassword: {
          required,
          sameAs: sameAs('password')
        }
      },
      validationGroup: ['credentials.username', 'credentials.password', 'credentials.confirmPassword']
    },
    computed: {
      formValidation: formValidation
    },
    methods: {
      spinner: function (bool) {
        this.loading = bool;
      },
      register: function (event) {
        event.preventDefault();
        var _self = this;
        if (_self.$v.validationGroup.$error) {
          return false;
        } else {
          _self.spinner(true);
          axios.get('http://localhost:8080/signup').then(function (res) {
            //console.log(res);
            _self.spinner(false);
          }).catch(function (err) {
            //console.log(err);
            _self.spinner(false);
          });
        }
      }
    }
  }

</script>
