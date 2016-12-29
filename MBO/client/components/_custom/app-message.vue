<template>
  <div class="form-message" :class="type" v-show="page.alert">
    <!--<pre>{{page}}</pre>-->
    <p>{{page.message}}</p>
    <ul v-show="page.details.length">
      <li v-for="detail in page.details">
        {{detail}}
      </li>
    </ul>
  </div>
</template>
<script>
  export default {
    name: 'app-message',
    computed: {
      page: function () {
        return this.$store.getters.page;
      },
      type: function () {
        return this.page.success ? 'success' : 'fail';
      }
    }
  }

</script>
<style>
  .form-message {
    color: #4c1f50;
  }
  
  .form-message.success {
    color: #4caf50;
  }
  
  .form-message.fail,
  .error {
    color: #ff5722;
  }
  
  .form-message ul {
    list-style: none;
    padding-left: 0px;
  }

</style>
