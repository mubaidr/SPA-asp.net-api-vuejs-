<template>
  <md-layout>
    <md-layout md-hide-small></md-layout>
    <md-layout>
      <task-create></task-create>
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
