import subprocess

# 定义训练配置文件的路径
config_file = "C:/Users/jingu/Desktop/RedeemedSoul/config.yaml"

# 定义两个训练命令
command_player_controller_ai = f"mlagents-learn {config_file} --run-id=PlayerControllerAI --train"
command_riru_ai = f"mlagents-learn {config_file} --run-id=RiruAI --train"

# 启动 PlayerControllerAI 的训练
print(f"Starting training for PlayerControllerAI...")
process_player_controller_ai = subprocess.Popen(command_player_controller_ai, shell=True)

# 启动 RiruAI 的训练
print(f"Starting training for RiruAI...")
process_riru_ai = subprocess.Popen(command_riru_ai, shell=True)

# 等待两个训练过程结束
process_player_controller_ai.wait()
process_riru_ai.wait()

print("Both training processes have completed.")