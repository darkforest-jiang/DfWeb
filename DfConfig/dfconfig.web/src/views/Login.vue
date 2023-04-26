<template>
    <div id="login">
        <div id="title">DarkForest 配置中心</div>
        <div></div>
        <el-input v-model="Logincode" placeholder="账号"></el-input>
        <el-input v-model="Loginpwd" placeholder="密码"></el-input>
        <el-button type="primary" @click="loginClick">登录</el-button>
    </div>
</template>

<script lang="ts">
    import { Options, Vue } from 'vue-class-component'
    @Options({
    })
    export default class Login extends Vue {
        public Logincode = ""
        public Loginpwd = ""
        public loginClick(): void{
            alert(this.Logincode)
        }
    }
</script>

<style lang="scss" scoped>
#login {
    position: absolute;
    height: 100%;
    width: 100%;
    margin: 0px;
    padding: 0px;
    background-color:rgb(41,143,204);
}
</style>