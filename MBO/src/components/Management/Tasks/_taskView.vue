<template>
  <div>
    <transition name="slide-up" appear>
      <div v-if="tasks.length">
        <transition-group class="full-width simple-list task-list" name="list-out-delayed" appear tag="ul">
          <li v-for="task in tasks" :key="task.MainTaskID">
            <task-card :task="task" :type="type"></task-card>
          </li>
        </transition-group>
      </div>
      <div v-else>
        Error
      </div>
    </transition>
  </div>
</template>
<script>
  import tasksService from 'services/tasks'
  import taskCard from 'components/_custom/task-card.vue'
  import pagination from 'components/_custom/pagination.vue'

  export default {
    name: 'task-view',
    components: {
      taskCard,
      pagination
    },
    props: ['type', 'active'],
    data () {
      return {
        tasks: [],
        count: 0,
        last_page: 0,
        fetched: false,
        loading: false
      }
    },
    watch: {
      active () {
        if (!this.fetched) {
          this.search()
        }
      }
    },
    computed: {},
    methods: {
      search () {
        this.loading = true
        tasksService
          .list(this.type)
          .then(res => {
            this.tasks = res.data.mainTask
            this.count = res.data.count
            this.last_page = res.data.last_page

            this.fetched = true
          })
          .catch(err => {
            console.log(err)
          })
          .then(() => {
            this.loading = false
          })
      }
    }
  }
</script>
<style>
  .task-list li {
    width: 19.5%;
    display: inline-block;
    margin-right: 0.5%;
  }

  .task-list li:last-child {
    margin-right: 0;
  }
</style>
