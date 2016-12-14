<template>
  <div>
    <div class="row">
      <div class="col-md-offset-4 col-md-4">
        <h1>Signup</h1>
        <p>Please provide following details to signup.</p>
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
          <input v-model="confirmPassword" type="password" class="form-control" placeholder="Confirm Password" v-model.trim="confirmPassword" @input="$v.confirmPassword.$touch()">
        </div>
        <!--<pre>validationGroup: {{ $v.validationGroup }}</pre>-->
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
  </div>
</template>
<script>
  import { required, sameAs, minLength, alphaNum } from 'vuelidate/lib/validators'
  import axios from 'axios'

  export default {
    data: function () {
      return {
        username: 'tester',
        password: 'tester',
        confirmPassword: 'tester'
      }
    },
    computed:{},
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
      register: function (event) {
        event.preventDefault();

        // console.log(axios);
        // console.log(this._data);
        // console.log(this.$v.username);

      }
    }
  }

</script>
