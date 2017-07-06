<template>
  <div>
    <!-- <pagination :lastpage="tasks.lastPage" :loading="tasks.loading" :count="tasks.length" @refresh="search"></pagination> -->
    <transition-group class="min-height no-padding full-width simple-list" name="list-out" appear tag="ul" v-if="tasks.length">
      <li class="list-out-item" v-for="task in tasks" :key="task.MainTaskID">
        {{task}}
      </li>
    </transition-group>
    <template v-else>
      Empty
    </template>
  </div>
</template>
<script>
import taskCard from 'components/_custom/task-card.vue'
import pagination from 'components/_custom/pagination.vue'
import { mapGetters, mapActions } from 'vuex'

export default {
  name: 'task-view',
  components: {
    taskCard,
    pagination
  },
  props: ['type', 'active'],
  data () {
    return {}
  },
  computed: {
    ...mapGetters(['tasks']),
    ...mapActions([])
  },
  methods: {
    search () {
      console.log('search')
    }
  }
}
</script>
<<style>

</style>
