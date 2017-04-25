<template>
  <md-layout md-gutter="">
    <md-layout md-hide-xsmall=""></md-layout>
    <md-layout md-hide-medium=""></md-layout>
    <md-layout>
      <div class="flex-vertical min-height full-width">
        <md-card class="full-width">
          <md-card-header class="indigo">
            <md-card-header-text>
              <div class="md-title">Create a new account</div>
              <span>Sign Up</span>
            </md-card-header-text>
            <md-spinner class="md-accent" md-indeterminate :md-stroke="4" v-show="state.loading"></md-spinner>
          </md-card-header>
          <md-card-content>
            <md-input-container :class="{'md-input-invalid': errors.has('Email')}">
              <label>Email</label>
              <md-input name="Email" data-vv-name="Email" data-vv-rules="required|email|min:6" type="email" v-model="credentials.Email" v-validate></md-input>
              <span class="md-error">{{errors.first('Email')}}</span>
            </md-input-container>
            <md-input-container md-has-password :class="{'md-input-invalid': errors.has('Password')}">
              <label>Password</label>
              <md-input name="Password" data-vv-name="Password" data-vv-rules="required|min:6" type="password" v-model="credentials.Password" v-validate></md-input>
              <span class="md-error">{{errors.first('Password')}}</span>
            </md-input-container>
            <md-input-container md-has-password :class="{'md-input-invalid': errors.has('ConfirmPassword')}">
              <label>Confirm Password</label>
              <md-input name="ConfirmPassword" data-vv-name="ConfirmPassword" data-vv-rules="required|confirmed:Password" type="password" v-model="credentials.ConfirmPassword" v-validate></md-input>
              <span class="md-error">{{errors.first('ConfirmPassword')}}</span>
            </md-input-container>
            <app-message :state="state"></app-message>
          </md-card-content>
          <md-card-actions>
            <router-link class="md-accent" tag="md-button" to="/signin">Already have an account?</router-link>
            <md-button class="md-raised md-accent" id="btnSubmit" @click.native="formValidate" :disabled="state.loading">Sign Up</md-button>
          </md-card-actions>
        </md-card>
      </div>
    </md-layout>
    <md-layout md-hide-xsmall=""></md-layout>
    <md-layout md-hide-medium=""></md-layout>
  </md-layout>
</template>
<script>
  import {
    signup
  } from 'services/account'

  export default {
    data () {
      return {
        credentials: {
          Email: 'tester@test.com',
          Password: 'tester1234',
          ConfirmPassword: 'tester1234'
        },
        state: {
          loading: false,
          type: 'error',
          title: null,
          details: null
        }
      }
    },
    methods: {
      setErrorDetails (err) {
        // console.dir(err);
  
        if (err) {
          this.state.title = err.message
          if (err.response && err.response.data && err.response.data.error_description) {
            this.state.title = err.response.data.error_description
          }
        } else {
          this.state.title = null
        }
      },
      formValidate (event) {
        event.preventDefault()
  
        this.$validator.validateAll().then(success => {
          if (!success) return

          this.setErrorDetails()

          this.state.loading = true

          signup(this.credentials).then(res => {
            this.$router.push({
              path: '/signin',
              params: {
                message: 'signup-success'
              }
            })
          }).catch(err => {
            this.setErrorDetails(err)
          }).then(() => {
            this.state.loading = false
          })
        })
      }
    }
  }

</script>
