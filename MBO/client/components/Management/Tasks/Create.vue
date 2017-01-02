<template>
  <md-layout>
    <md-layout md-hide-small></md-layout>
    <md-layout>
      <create-task></create-task>
    </md-layout>
    <md-layout md-hide-small></md-layout>
  </md-layout>
</template>
<script>
  import taskCreate from 'components/Management/Tasks/_partial_create.vue'

  export default {
    components: {
      'task-create': taskCreate
    }
  }

</script>
