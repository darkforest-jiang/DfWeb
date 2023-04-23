import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'

//导入element-plus
import ElementPlus from 'element-plus'
import 'element-plus/theme-chalk/index.css'

createApp(App).use(store).use(router).use(ElementPlus).mount('#app')
