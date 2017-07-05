<template>
  <div class="form-message" :class="state.type" v-show="show">
    <span>{{state.title}}!</span>
    <br>
    <span>{{state.details}}</span>
  </div>
</template>
<script>
  export default {
    name: 'app-message',
    props: ['state'],
    data () {
      return {
        show: false
      }
    },
    watch: {
      'state.title' (val) {
        this.show = !(val === '' || val === null || val === undefined)
      }
    }
  }

</script>
<style scoped>
  .form-message.info {
    color: #4c1f50;
  }
  
  .form-message.success {
    color: #4caf50;
  }
  
  .form-message.error {
    color: #ff5722;
  }
  
  .form-message ul {
    list-style: none;
    padding-left: 0px;
  }
</style>
