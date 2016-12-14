<template>
  <div>
    <div class="row">
      <div class="col-md-offset-4 col-md-4">
        <h1>Signup</h1>
        <p>Please provide following details to signup.</p>
        <div v-show="$v.validationGroup.$invalid" class="text-danger">
          <p>Please fix following issues and then try to submit!</p>
          <ul class="validation-list">
            {{validationMessage}}
          </ul>
        </div>
        <div class="form-group" v-bind:class="{ 'has-error': $v.username.$error }">
          <label for="Username" class="control-label">Username</label>
          <input v-model="username" type="text" class="form-control" placeholder="Username" v-model.trim="username" @input="$v.username.$touch()">
        </div>
        <div class="form-group" v-bind:class="{ 'has-error': $v.password.$error }">
          <label for="Password" class="control-label">Password</label>
          <input v-model="password" type="password" class="form-control" placeholder="Password" v-model.trim="password" @input="$v.password.$touch()">
        </div>
        <div class="form-group" v-bind:class="{ 'has-error': $v.confirmPassword.$error }">
          <label for="ConfirmPassword" class="control-label">Confirm Password</label>
          <input v-model="confirmPassword" type="password" class="form-control" placeholder="Confirm Password" v-model.trim="confirmPassword"
            @input="$v.confirmPassword.$touch()">
        </div>
        <div class="form-group">
          <button v-on:click="register" class="btn btn-block btn-primary" v-bind:disabled="$v.validationGroup.$invalid || !$v.validationGroup.$dirty">Create Account</button>
        </div>
        <div class="form-group">
          Already have an account?
          <router-link to="/login">Signin here!</router-link>
          <br/> Forgot password?
          <router-link to="/recover">Recover now!</router-link>
        </div>
      </div>
    </div>
    <div v-show="loading" class="backdrop">
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

  export default {
    data: function () {
      return {
        username: 'tester',
        password: 'tester1234',
        confirmPassword: 'tester1234',
        loading: false,
        successMessage: ''
      }
    },
    components: {
      RingLoader
    },
    computed: {
      validationMessage: function () {
        return this.$v.validationGroup;
      }
    },
    validations: {
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
      },
      validationGroup: ['username', 'password', 'confirmPassword']
    },
    methods: {
      spinner: function (bool) {
        this.loading = bool;
      },
      register: function (event) {
        var _self = this;
        event.preventDefault();
        _self.spinner(true);
        axios.get('http://localhost:8080/signup').then(function (res) {
          console.log(res);
          _self.spinner(false);
        }).catch(function (err) {
          console.log(err);
          _self.spinner(false);
        });
      }
    }
  }

</script>
