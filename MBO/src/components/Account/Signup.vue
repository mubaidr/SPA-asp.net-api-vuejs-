<template>
  <md-layout md-gutter>
    <md-layout></md-layout>
    <md-layout md-flex-large="33" md-flex-medium="50" md-flex-small="75" md-flex-xsmall="100">
      <md-whiteframe md-tag="section" class="form">
        <md-progress class="md-accent" :class="{'hidden': !state.loading}" md-indeterminate></md-progress>
        <div class="form-container">
          <h1>
            <span>Register</span>
            <br/>
            <span class="md-caption">Get started</span>
          </h1>
          <md-input-container :class="{'md-input-invalid': errors.has('Email')}">
            <label>Email</label>
            <md-input name="Email" data-vv-name="Email" data-vv-rules="required|email|min:6" type="email" v-model="credentials.Email" v-validate :disabled="state.loading"></md-input>
            <span class="md-error">{{errors.first('Email')}}</span>
          </md-input-container>
          <md-input-container md-has-password :class="{'md-input-invalid': errors.has('Password')}">
            <label>Password</label>
            <md-input name="Password" data-vv-name="Password" data-vv-rules="required|min:6" type="password" v-model="credentials.Password" v-validate :disabled="state.loading"></md-input>
            <span class="md-error">{{errors.first('Password')}}</span>
          </md-input-container>
          <md-input-container md-has-password :class="{'md-input-invalid': errors.has('ConfirmPassword')}">
            <label>Confirm Password</label>
            <md-input name="ConfirmPassword" data-vv-name="ConfirmPassword" data-vv-rules="required|confirmed:Password" type="password" v-model="credentials.ConfirmPassword" v-validate :disabled="state.loading"></md-input>
            <span class="md-error">{{errors.first('ConfirmPassword')}}</span>
          </md-input-container>
          <md-button class="md-raised md-accent" id="btnSubmit" @click.native="formValidate" :disabled="state.loading">Register</md-button>
        </div>
        <div class="text-right">
          <router-link class="md-accent" tag="md-button" to="/signin">Already have an account?</router-link>
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
          Email: 'tester@test.com',
          Password: 'tester1234',
          ConfirmPassword: 'tester1234'
        },
        state: {
          loading: false
        }
      }
    },
    methods: {
      ...mapActions(['signup']),
      formValidate (event) {
        event.preventDefault()
        this.$validator.validateAll().then(success => {
          if (!success) return
          this.state.loading = true

          this.signup(this.credentials).catch(err => {
            if (err.response && err.response.status === 400) {
              this.$toast.error({
                title: err.response.data.Message,
                message: err.response.data.ModelState[''][0],
                timeOut: 2500
              })
            } else {
              this.$toast.error({
                title: 'Error',
                message: 'An unknown error occurred'
              })
            }
          }).then(() => {
            this.state.loading = false
          })
        })
      }
    }
  }

</script>
