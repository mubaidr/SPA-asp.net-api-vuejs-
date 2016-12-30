<template>
  <div class="form-message" :class="page.type" v-show="page.alert">
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
      }
    }
  }

</script>
<style>
  .form-message.info {
    color: #4c1f50;
  }
  
  .form-message.success {
    color: #4caf50;
  }
  
  .form-message.fail {
    color: #ff5722;
  }
  
  .form-message ul {
    list-style: none;
    padding-left: 0px;
  }

</style>
