<template>
  <div>
    <md-layout>
      <md-layout md-hide-small=""></md-layout>
      <md-layout>
        <task-create></task-create>
      </md-layout>
      <md-layout md-hide-small="">
      </md-layout>
    </md-layout>
  </div>
</template>
<script>
import taskCreate from 'components/Management/Tasks/_partial_create.vue'

export default {
  components: {
    taskCreate
  }
}

</script>
