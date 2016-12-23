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
          <md-input-container :class="{'md-input-invalid': $v.credentials.email.$error}">
            <label>Username</label>
            <md-input v-model="credentials.email" type="email" name="email" v-model.trim="credentials.email" @input="$v.credentials.email.$touch()"></md-input>
            <div class="md-error">
              <span :class="{'not-valid': !$v.credentials.email.$required}">Required.</span>
              <span :class="{'not-valid': !$v.credentials.email.$alpha}">Should not contain numbers.</span>
              <span :class="{'not-valid': !$v.credentials.email.$minLength}">Minimum length should be 6.</span>
            </div>
          </md-input-container>
          <md-input-container md-has-password :class="{'md-input-invalid': $v.credentials.email.$error}">
            <label>Password</label>
            <md-input v-model="credentials.password" type="password" name="password" v-model.trim="credentials.password" @input="$v.credentials.password.$touch()"></md-input>
            <!--<span class="md-error">Test</span>-->
          </md-input-container>
          <md-input-container md-has-password :class="{'md-input-invalid': $v.credentials.email.$error}">
            <label>Confirm Password</label>
            <md-input v-model="credentials.confirmPassword" type="password" name="confirmPassword" v-model.trim="credentials.confirmPassword"
              @input="$v.credentials.confirmPassword.$touch()"></md-input>
            <!--<span class="md-error">Test</span>-->
          </md-input-container>
        </md-card-content>
        <md-card-actions>
          <router-link tag="md-button" to="/signin" class="md-accent">Already have an account?</router-link>
          <md-button class="md-raised md-accent" @click="signup">Register</md-button>
        </md-card-actions>
        <md-card-content>
          <div v-show="progress.statusMessage" class="alert alert-danger">
            {{progress.statusMessage}}
          </div><pre>{{$v.credentials.email}}</pre>
        </md-card-content>
      </md-card>
    </md-layout>
    <md-layout md-hide-small></md-layout>
  </md-layout>
</template>
<script>
  import {
    required,
    sameAs,
    minLength,
    alphaNum,
    alpha
  } from 'vuelidate/lib/validators'
  //import email from 'utilities/validators/email'
  import axios from 'axios'
  //import 'utilities/string'
  // import {
  //   Summary
  // } from 'utilities/validationSummary'
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
          alpha,
          minLength: minLength(5)
        },
        password: {
          required,
          alphaNum,
          minLength: minLength(5)
        },
        confirmPassword: {
          required,
          sameAs: sameAs('password')
        }
      }
    },
    computed: {
      //validationSummary: Summary,
      getFirstError: function (model) {
        console.log(this, model);
        var _self = this;
        if (!model) return;
        var val = _self.$v[model];
        if (!val.$error) return;
        //var val

      }
    },
    methods: {
      signup: function (event) {
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
