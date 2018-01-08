<template>
  <md-layout md-gutter>
    <md-layout></md-layout>
    <md-layout md-flex-large="33" md-flex-medium="50" md-flex-small="75" md-flex-xsmall="100">

      <md-whiteframe md-tag="section" class="form">
        <md-progress class="md-accent" :class="{'hidden': !state.loading}" md-indeterminate></md-progress>
        <div class="form-container">
          <h1>
            <span>Signin</span>
            <br/>
            <span class="md-caption">Welcome back</span>
          </h1>
          <md-input-container :class="{'md-input-invalid': errors.has('Email')}">
            <label>Email</label>
            <md-input name="Email" data-vv-name="Email" data-vv-rules="required|email|min:6" type="email" v-model="credentials.UserName" v-validate :disabled="state.loading"></md-input>
            <span class="md-error">{{errors.first('Email')}}</span>
          </md-input-container>
          <md-input-container md-has-password :class="{'md-input-invalid': errors.has('Password')}">
            <label>Password</label>
            <md-input name="Password" data-vv-name="Password" data-vv-rules="required|min:6" type="password" v-model="credentials.Password" v-validate :disabled="state.loading"></md-input>
            <span class="md-error">{{errors.first('Password')}}</span>
          </md-input-container>
          <md-button class="md-raised md-accent" id="btnSubmit" @click.native="formValidate" :disabled="state.loading">Singin</md-button>
        </div>
        <div class="text-right">
          <router-link class="md-accent" tag="md-button" to="/recover">Forgot password?</router-link>
          <router-link class="md-accent" tag="md-button" to="/signup">Don't have an account?</router-link>
        </div>
      </md-whiteframe>
    </md-layout>
    <md-layout></md-layout>
  </md-layout>
</template>
<script>
  import { mapActions } from 'vuex'

  export default {
    data () {
      return {
        credentials: {
          UserName: 'tester@test.com',
          Password: 'tester1234'
        },
        state: {
          loading: false
        }
      }
    },
    methods: {
      ...mapActions(['signin']),
      formValidate (event) {
        event.preventDefault()
        this.$validator.validateAll().then(success => {
          if (!success) return
          this.state.loading = true
          this.signin(this.credentials)
            .catch(err => {
              console.dir(err)
              if (err.response && err.response.status === 400) {
                this.$toast.error({
                  title: 'Signin failed',
                  message: err.response.data.error_description
                })
              } else {
                this.$toast.error({
                  title: 'Error',
                  message: 'An unknown error occurred'
                })
              }
            })
            .then(() => {
              this.state.loading = false
            })
        })
      }
    }
  }
</script>
