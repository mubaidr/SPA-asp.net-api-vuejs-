<template>
  <div>
    <pagination :lastpage="TaskList.lastPage" :loading="TaskList.loading" :count="TaskList.count" :view-menu="true" :compact="true" :full-width="true" @refresh="search"></pagination>
    <md-layout md-gutter="16">
      <md-layout v-show="activeView == 'List'" md-hide-small></md-layout>
      <md-layout md-gutter="16">
        <transition-group class="min-height no-padding full-width simple-list" name="list-out" appear tag="ul" v-if="TaskList.content.length">
          <li class="list-out-item min-width" :class="{'full-width' : activeView == 'List'}" v-for="Task in TaskList.content" v-bind:key="Task.MainTaskID">
            <task-card @remove-task-item="removeTaskItem" :Task="Task" :Type="TaskList.name" v-if="activeView == 'Card'"></task-card>
            <task-list-item @remove-task-item="removeTaskItem" :Task="Task" :Type="TaskList.name" v-else></task-list-item>
          </li>
        </transition-group>
      </md-layout>
      <md-layout v-show="activeView == 'List'" md-hide-small></md-layout>
    </md-layout>
  </div>
</template>

<script>
  import pagination from 'components/_custom/pagination.vue'
  import taskCard from 'components/_custom/task-card.vue'
  import taskListItem from 'components/_custom/task-list-item.vue'

  import {
    listAssigned,
    listCreated,
    listCompleted,
    listTrash
  } from 'services/tasks'

  export default {

  }
</script>

<style>
  
</style>
