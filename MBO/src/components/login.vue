<template id="login">
  <div>
    <div class="row">
      <div class="col-md-offset-4 col-md-4">
        <form>
          <h1>Login</h1>
          <p>Please enter username and password to continue.</p>
          <div class="form-group">
            <label for="Username" class="control-label">Username</label>
            <input v-model="username" type="text" class="form-control" id="Username" placeholder="Username">
          </div>
          <div class="form-group">
            <label for="Password" class="control-label">Password</label>
            <input v-model="password" type="password" class="form-control" id="Password" placeholder="Password">
            <div class="checkbox">
              <label>
                    <input type="checkbox"/> Remember me
                  </label>
            </div>
          </div>
          <div class="form-group">
            <button v-on:click="login" class="btn btn-primary btn-block">Login to my account</button>
          </div>
          <div class="form-group">Don't have a account?
            <router-link to="/signup">Signup here!</router-link>
            <br/> Forgot password?
            <router-link to="/recover">Recover now!</router-link>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>
<script>
  export default {
    template: '#login',
    name: 'login',
    data: function (){
      return {
        username: 'test',
        password: 'asdf1234'
      }
    },
    methods:{
      login: function(event){
        event.preventDefault();
        this.$http.get('http://localhost/Beta/api/values').then((response) => {
          console.log('success', response);
        }, (response) => {
          console.log('fail', response);
        });
      }
    }
  }
</script>
